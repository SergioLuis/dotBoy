using DotBoy.Debugging.CommandLine;

namespace DotBoy.Debugging.CommandLine.Commands
{
    internal class MemoryCommand : InteractiveCommand
    {
        public override string Name =>
            Localization.GetString(Localization.Names.MemoryCommandName);

        public override string Description =>
            Localization.GetString(Localization.Names.MemoryCommandDescription);

        public override string Prompt =>
            Localization.GetString(Localization.Names.MemoryCommandPrompt);

        internal MemoryCommand()
        {
            mSubcommands.Add(new ReadMemoryCommand());
            mSubcommands.Add(new WriteMemoryCommand());
        }

        internal class ReadMemoryCommand : InteractiveCommand
        {
            public override string Name =>
                Localization.GetString(Localization.Names.ReadMemoryCommandName);

            public override string Description =>
                Localization.GetString(Localization.Names.ReadMemoryCommandDescription);

            public override string Prompt =>
                Localization.GetString(Localization.Names.ReadMemoryCommandPrompt);

            internal ReadMemoryCommand()
            {
                mSubcommands.Add(new ExecuteReadCommand());
            }

            internal class ExecuteReadCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteReadMemoryCommandName);

                public override string Description =>
                    Localization.GetString(Localization.Names.ExecuteReadMemoryCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 1)
                        return false;

                    // Cast is done with signed long to make sure it is a number.
                    // Ranges are checked while executing the command, so the
                    // error message is more meaningful that the command description,
                    // which doesn't have access to the actual memory size.
                    return long.TryParse(args[0], out _);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    if (!long.TryParse(args[0], out long address))
                    {
                        ConsoleIO.WriteLine(
                            Localization.GetString(
                                Localization.Names.ExecuteReadMemoryCommandInvalidAddressMessage,
                                items.Memory.Size));
                        return;
                    }

                    if (address > items.Memory.Size - 1)
                    {
                        ConsoleIO.WriteLine(
                            Localization.GetString(
                                Localization.Names.ExecuteReadMemoryCommandInvalidAddressMessage,
                                items.Memory.Size));
                        return;
                    }

                    ConsoleIO.WriteLine(
                        $"M[{address}] -> {ConsoleIO.FormatNumber(items.Memory[(ushort)address])}");
                }
            }
        }

        internal class WriteMemoryCommand : InteractiveCommand
        {
            public override string Name =>
                Localization.GetString(Localization.Names.WriteMemoryCommandName);

            public override string Description =>
                Localization.GetString(Localization.Names.WriteMemoryCommandDescription);

            public override string Prompt =>
                Localization.GetString(Localization.Names.WriteMemoryCommandPrompt);

            internal WriteMemoryCommand()
            {
                mSubcommands.Add(new ExecuteWriteCommand());
            }

            internal class ExecuteWriteCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteWriteMemoryCommandName);

                public override string Description =>
                    Localization.GetString(Localization.Names.ExecuteWriteMemoryCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 2)
                        return false;

                    // Cast is done with signed long to make sure it is a number.
                    // Ranges are checked while executing the command, so the
                    // error message is more meaningful that the command description,
                    // which doesn't have access to the actual memory size.
                    return long.TryParse(args[0], out _)
                        && long.TryParse(args[1], out _);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    long address = long.Parse(args[0]);
                    if (address > items.Memory.Size - 1)
                    {
                        ConsoleIO.WriteLine(
                            Localization.GetString(
                                Localization.Names.ExecuteWriteMemoryCommandInvalidAddressMessage,
                                items.Memory.Size));
                        return;
                    }

                    if (!byte.TryParse(args[1], out byte data))
                    {
                        ConsoleIO.WriteLine(
                            Localization.GetString(
                                Localization.Names.ExecuteWriteMemoryCommandInvalidDataMessage,
                                byte.MaxValue));
                        return;
                    }

                    items.Memory[(ushort)address] = data;

                    ConsoleIO.WriteLine($"M[{address}] <- {ConsoleIO.FormatNumber(data)}");
                }
            }
        }
    }
}
