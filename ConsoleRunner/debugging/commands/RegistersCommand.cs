using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleRunner.Debugging.Commands
{
    internal class RegistersCommand : InteractiveCommand
    {
        public override string Name =>
            Localization.GetString(Localization.Names.RegistersCommandName);

        public override string Description =>
            Localization.GetString(Localization.Names.RegistersCommandDescription);

        public override string Prompt =>
            Localization.GetString(Localization.Names.RegistersCommandPrompt);

        internal RegistersCommand()
        {
            mSubcommands = new CommandCollection();
            mSubcommands.AddCommand(new ReadRegisterCommand());
            mSubcommands.AddCommand(new WriteRegisterCommand());
        }

        internal class ReadRegisterCommand : InteractiveCommand
        {
            public override string Name =>
                Localization.GetString(Localization.Names.ReadRegisterCommandName);

            public override string Description =>
                Localization.GetString(Localization.Names.ReadRegisterCommandDescription);

            public override string Prompt =>
                Localization.GetString(Localization.Names.ReadRegisterCommandPrompt);

            internal ReadRegisterCommand()
            {
                mSubcommands = new CommandCollection();
                mSubcommands.AddCommand(new ExecuteReadCommand());
            }

            internal class ExecuteReadCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteReadRegisterCommandName);

                public override string Description =>
                    Localization.GetString(
                        Localization.Names.ExecuteReadRegisterCommandDescription,
                        string.Join(", ", Constants.Registers.Names));

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 1)
                        return false;

                    var validRegisters = new HashSet<string>(
                        Constants.Registers.Names,
                        StringComparer.InvariantCultureIgnoreCase);

                    return (validRegisters.Contains(args[0]));
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    switch (args[0].ToLowerInvariant())
                    {
                        case Constants.Registers.A:
                            ConsoleIO.WriteLine($"a -> {items.Registers.A}");
                            return;

                        case Constants.Registers.F:
                            ConsoleIO.WriteLine($"f -> {items.Registers.F}");
                            return;

                        case Constants.Registers.B:
                            ConsoleIO.WriteLine($"b -> {items.Registers.B}");
                            return;

                        case Constants.Registers.C:
                            ConsoleIO.WriteLine($"c -> {items.Registers.C}");
                            return;

                        case Constants.Registers.D:
                            ConsoleIO.WriteLine($"d -> {items.Registers.D}");
                            return;

                        case Constants.Registers.E:
                            ConsoleIO.WriteLine($"e -> {items.Registers.E}");
                            return;

                        case Constants.Registers.H:
                            ConsoleIO.WriteLine($"h -> {items.Registers.H}");
                            return;

                        case Constants.Registers.L:
                            ConsoleIO.WriteLine($"l -> {items.Registers.L}");
                            return;

                        case Constants.Registers.PC:
                            ConsoleIO.WriteLine($"pc -> {items.Registers.PC}");
                            return;

                        case Constants.Registers.SP:
                            ConsoleIO.WriteLine($"sp -> {items.Registers.SP}");
                            return;
                    }

                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.InvalidRegisterErrorMessage,
                            args[0], string.Join(", ", Constants.Registers.Names)));
                }
            }
        }

        internal class WriteRegisterCommand : InteractiveCommand
        {
            public override string Name =>
                Localization.GetString(Localization.Names.WriteRegisterCommandName);

            public override string Description =>
                Localization.GetString(Localization.Names.WriteRegisterCommandDescription);

            public override string Prompt =>
                Localization.GetString(Localization.Names.WriteRegisterCommandPrompt);

            internal WriteRegisterCommand()
            {
                mSubcommands = new CommandCollection();
                mSubcommands.AddCommand(new ExecuteWriteCommand());
            }

            internal class ExecuteWriteCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteWriteRegisterCommandName);

                public override string Description =>
                    Localization.GetString(
                        Localization.Names.ExecuteWriteRegisterCommandDescription,
                        string.Join(", ", Constants.Registers.Names));

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 2)
                        return false;

                    var validRegisters = new HashSet<string>(
                        Constants.Registers.Names,
                        StringComparer.InvariantCultureIgnoreCase);

                    if (!validRegisters.Contains(args[0]))
                        return false;

                    return ushort.TryParse(args[1], out _);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    ushort newValue = ushort.Parse(args[1]);
                    switch (args[0].ToLowerInvariant())
                    {
                        case Constants.Registers.A:
                            items.Registers.A = (byte)newValue;
                            ConsoleIO.WriteLine($"a <- {newValue}");
                            return;

                        case Constants.Registers.F:
                            items.Registers.F = (byte)newValue;
                            ConsoleIO.WriteLine($"f <- {newValue}");
                            return;

                        case Constants.Registers.B:
                            items.Registers.B = (byte)newValue;
                            ConsoleIO.WriteLine($"b <- {newValue}");
                            return;

                        case Constants.Registers.C:
                            items.Registers.C = (byte)newValue;
                            ConsoleIO.WriteLine($"c <- {newValue}");
                            return;

                        case Constants.Registers.D:
                            items.Registers.D = (byte)newValue;
                            ConsoleIO.WriteLine($"d <- {newValue}");
                            return;

                        case Constants.Registers.E:
                            items.Registers.E = (byte)newValue;
                            ConsoleIO.WriteLine($"e <- {newValue}");
                            return;

                        case Constants.Registers.H:
                            items.Registers.H = (byte)newValue;
                            ConsoleIO.WriteLine($"h <- {newValue}");
                            return;

                        case Constants.Registers.L:
                            items.Registers.L = (byte)newValue;
                            ConsoleIO.WriteLine($"l <- {newValue}");
                            return;

                        case Constants.Registers.PC:
                            items.Registers.PC = newValue;
                            ConsoleIO.WriteLine($"pc <- {newValue}");
                            return;

                        case Constants.Registers.SP:
                            items.Registers.SP = newValue;
                            ConsoleIO.WriteLine($"sp <- {newValue}");
                            return;
                    }

                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.InvalidRegisterErrorMessage,
                            args[0], string.Join(", ", Constants.Registers.Names)));
                }
            }
        }
    }
}
