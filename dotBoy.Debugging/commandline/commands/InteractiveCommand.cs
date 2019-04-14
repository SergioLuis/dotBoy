using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DotBoy.Debugging.CommandLine;

namespace DotBoy.Debugging.CommandLine
{
    internal abstract class InteractiveCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Prompt { get; }

        public IEnumerable<ICommand> Subcommands
        {
            get { return mSubcommands.AsReadOnly(); }
        }

        public void AddSubcommand(ICommand command)
        {
            mSubcommands.Add(command);
        }

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
            if (!Subcommand.TryGetCommand( mSubcommands, trimmedArgs, items, out ICommand subcommand))
            {
                Console.Error.WriteLine(Subcommand.GetHelpForAll(mSubcommands));
                return;
            }

            subcommand.Execute(trimmedArgs, items);
        }

        public virtual void ExecuteInteractive(DebuggingItems items)
        {
            string input;
            string exitKeyword = Localization.GetString(Localization.Names.Exit);

            Action<string> tabAction = (string s) => PrintHelp(s, items);

            while ((input = ConsoleIO.ReadStringInteractive(Prompt, ConsoleKey.Tab, tabAction)) != exitKeyword)
            {
                string[] args = ConsoleIO.Split(input);
                if (!Subcommand.TryGetCommand(mSubcommands, args, items, out ICommand command))
                {
                    Console.Error.WriteLine(Subcommand.GetHelpForAll(mSubcommands));
                    continue;
                }

                command.Execute(args, items);
            }
        }

        void PrintHelp(string userInput, DebuggingItems items)
        {
            string[] args = ConsoleIO.Split(userInput);
            Console.Error.WriteLine(Subcommand.GetHelpForCommandLine(mSubcommands, args, items));
        }

        static string GetCompletedCommandLine(string line)
        {
            if (string.IsNullOrEmpty(line))
                return string.Empty;

            int lastSpaceIndex = line.LastIndexOf(' ');
            if (lastSpaceIndex == -1)
                return line;

            return line.Substring(0, lastSpaceIndex).Trim();
        }

        protected readonly List<ICommand> mSubcommands = new List<ICommand>();

        static class Subcommand
        {
            internal static bool TryGetCommand(
                IEnumerable<ICommand> commands,
                string[] args,
                DebuggingItems items,
                out ICommand cmd)
            {
                foreach (var command in commands)
                {
                    if (!command.CanExecute(args, items))
                        continue;

                    cmd = command;
                    return true;
                }

                cmd = null;
                return false;
            }

            internal static string GetHelpForAll(IEnumerable<ICommand> commands)
            {
                var commandsAndDescriptions = new List<Tuple<string, string>>();
                var maxNameLength = int.MinValue;

                foreach (var registeredCommand in commands)
                {
                    commandsAndDescriptions.Add(
                        new Tuple<string, string>(
                            registeredCommand.Name,
                            registeredCommand.Description));

                    if (registeredCommand.Name.Length > maxNameLength)
                        maxNameLength = registeredCommand.Name.Length;
                }

                var result = new StringBuilder();
                foreach (var tuple in commandsAndDescriptions)
                {
                    result.AppendFormat(
                        " {0} : {1}{2}",
                        tuple.Item1.PadLeft(maxNameLength),
                        tuple.Item2,
                        Environment.NewLine);
                }

                return result.ToString();
            }

            internal static string GetHelpForCommandLine(
                IEnumerable<ICommand> commands, string[] args, DebuggingItems items)
            {
                ICommand command = GetLastRecognisedCommand(commands, args, items);
                switch (command)
                {
                    case FinalCommand finalCommand:
                        return finalCommand.Description;

                    case InteractiveCommand interactiveCommand:
                        return GetHelpForAll(interactiveCommand.Subcommands);
                }

                return GetHelpForAll(commands);
            }

            static ICommand GetLastRecognisedCommand(
                IEnumerable<ICommand> commands, string[] args, DebuggingItems items)
            {
                foreach (var command in commands)
                {
                    if (!command.CanExecute(args, items))
                        continue;

                    InteractiveCommand interactiveCommand =
                        command as InteractiveCommand;

                    if (interactiveCommand == null)
                        return command;

                    ICommand result = GetLastRecognisedCommand(
                        interactiveCommand.Subcommands,
                        args.Skip(1).ToArray(),
                        items);

                    return result ?? interactiveCommand;
                }

                return null;
            }
        }
    }
}
