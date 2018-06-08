using System;
using System.Linq;

namespace ConsoleRunner.Debugging.Commands
{
    internal abstract class InteractiveCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Prompt { get; }

        public virtual bool CanExecute(string[] args, DebuggingItems items)
        {
            if (args.Length == 0)
                return false;

            return Name.StartsWith(
                args[0],
                StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual void Execute(string[] args, DebuggingItems items)
        {
            if (args.Length == 1)
            {
                ExecuteInteractive(items);
                return;
            }

            string[] trimmedArgs = args.Skip(1).ToArray();

            if (!mSubcommands.TryGetCommand(trimmedArgs, items, out ICommand subcommand))
            {
                Console.Error.WriteLine(mSubcommands.GetHelp());
                return;
            }

            subcommand.Execute(trimmedArgs, items);
        }

        public virtual void ExecuteInteractive(DebuggingItems items)
        {
            string input;
            string exitKeyword = Localization.GetString(Localization.Names.Exit);
            while ((input = ConsoleIO.ReadString(Prompt)) != exitKeyword)
            {
                string[] args = ConsoleIO.Split(input);
                if (!mSubcommands.TryGetCommand(args, items, out ICommand command))
                {
                    Console.Error.WriteLine(mSubcommands.GetHelp());
                    continue;
                }

                command.Execute(args, items);
            }
        }

        protected CommandCollection mSubcommands;
    }
}
