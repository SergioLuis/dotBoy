using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRunner.Debugging.Commands
{
    internal class CommandCollection
    {
        public CommandCollection()
        {
            mCommands = new List<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            mCommands.Add(command);
        }

        public bool TryGetCommand(
            string[] args,
            DebuggingItems items,
            out ICommand cmd)
        {
            foreach (var registeredCommand in mCommands)
            {
                if (!registeredCommand.CanExecute(args, items))
                    continue;

                cmd = registeredCommand;
                return true;
            }

            cmd = null;
            return false;
        }

        public string GetHelp()
        {
            var commandsAndDescriptions = new List<Tuple<string, string>>();
            var maxNameLength = int.MinValue;

            foreach (var registeredCommand in mCommands)
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
                    " {0}: {1}{2}",
                    tuple.Item1.PadLeft(maxNameLength),
                    tuple.Item2,
                    Environment.NewLine);
            }

            return result.ToString();
        }

        readonly List<ICommand> mCommands;
    }
}
