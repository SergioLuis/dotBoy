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
            mSubcommands.Add(new ReadRegisterCommand());
            mSubcommands.Add(new WriteRegisterCommand());
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
                mSubcommands.Add(new ExecuteReadOneCommand());
                mSubcommands.Add(new ExecuteReadAllCommand());
                mSubcommands.Add(new ExecuteReadFlagsCommand());
            }

            internal class ExecuteReadOneCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteReadOneRegisterCommandName);

                public override string Description =>
                    Localization.GetString(
                        Localization.Names.ExecuteReadOneRegisterCommandDescription,
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
                            ConsoleIO.WriteLine($"a -> {ConsoleIO.FormatNumber(items.Registers.A)}");
                            return;

                        case Constants.Registers.F:
                            ConsoleIO.WriteLine($"f -> {ConsoleIO.FormatNumber(items.Registers.F)}");
                            return;

                        case Constants.Registers.B:
                            ConsoleIO.WriteLine($"b -> {ConsoleIO.FormatNumber(items.Registers.B)}");
                            return;

                        case Constants.Registers.C:
                            ConsoleIO.WriteLine($"c -> {ConsoleIO.FormatNumber(items.Registers.C)}");
                            return;

                        case Constants.Registers.D:
                            ConsoleIO.WriteLine($"d -> {ConsoleIO.FormatNumber(items.Registers.D)}");
                            return;

                        case Constants.Registers.E:
                            ConsoleIO.WriteLine($"e -> {ConsoleIO.FormatNumber(items.Registers.E)}");
                            return;

                        case Constants.Registers.H:
                            ConsoleIO.WriteLine($"h -> {ConsoleIO.FormatNumber(items.Registers.H)}");
                            return;

                        case Constants.Registers.L:
                            ConsoleIO.WriteLine($"l -> {ConsoleIO.FormatNumber(items.Registers.L)}");
                            return;

                        case Constants.Registers.PC:
                            ConsoleIO.WriteLine($"pc -> {ConsoleIO.FormatNumber(items.Registers.PC)}");
                            return;

                        case Constants.Registers.SP:
                            ConsoleIO.WriteLine($"sp -> {ConsoleIO.FormatNumber(items.Registers.SP)}");
                            return;
                    }

                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.InvalidRegisterErrorMessage,
                            args[0], string.Join(", ", Constants.Registers.Names)));
                }
            }

            internal class ExecuteReadAllCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteReadAllRegistersCommandName);

                public override string Description =>
                    Localization.GetString(Localization.Names.ExecuteReadAllRegistersCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 1)
                        return false;

                    return Name.StartsWith(
                        args[0],
                        StringComparison.InvariantCultureIgnoreCase);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    ConsoleIO.WriteLine($"{Constants.Registers.A} -> {ConsoleIO.FormatNumber(items.Registers.A)}");
                    ConsoleIO.WriteLine($"{Constants.Registers.F} -> {ConsoleIO.FormatNumber(items.Registers.F)}");
                    ConsoleIO.WriteLine($"{Constants.Registers.B} -> {ConsoleIO.FormatNumber(items.Registers.B)}");
                    ConsoleIO.WriteLine($"{Constants.Registers.C} -> {ConsoleIO.FormatNumber(items.Registers.C)}");
                    ConsoleIO.WriteLine($"{Constants.Registers.D} -> {ConsoleIO.FormatNumber(items.Registers.D)}");
                    ConsoleIO.WriteLine($"{Constants.Registers.E} -> {ConsoleIO.FormatNumber(items.Registers.E)}");
                    ConsoleIO.WriteLine($"{Constants.Registers.H} -> {ConsoleIO.FormatNumber(items.Registers.H)}");
                    ConsoleIO.WriteLine($"{Constants.Registers.L} -> {ConsoleIO.FormatNumber(items.Registers.L)}");
                    ConsoleIO.WriteLine($"{Constants.Registers.SP} -> {ConsoleIO.FormatNumber(items.Registers.SP)}");
                    ConsoleIO.WriteLine($"{Constants.Registers.PC} -> {ConsoleIO.FormatNumber(items.Registers.PC)}");
                }
            }

            internal class ExecuteReadFlagsCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteReadFlagsCommandName);

                public override string Description =>
                    Localization.GetString(Localization.Names.ExecuteReadFlagsCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 1)
                        return false;

                    return Name.StartsWith(
                        args[0],
                        StringComparison.InvariantCultureIgnoreCase);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    ConsoleIO.WriteLine($"{Constants.Flags.Zero} -> {items.Registers.FlagZ}");
                    ConsoleIO.WriteLine($"{Constants.Flags.Subs} -> {items.Registers.FlagN}");
                    ConsoleIO.WriteLine($"{Constants.Flags.Half} -> {items.Registers.FlagH}");
                    ConsoleIO.WriteLine($"{Constants.Flags.Full} -> {items.Registers.FlagCY}");
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
                mSubcommands.Add(new ExecuteWriteOneCommand());
                mSubcommands.Add(new ExecuteWriteFlagsCommand());
                mSubcommands.Add(new ExecuteResetAllCommand());
            }

            internal class ExecuteWriteOneCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteWriteOneRegisterCommandName);

                public override string Description =>
                    Localization.GetString(
                        Localization.Names.ExecuteWriteOneRegisterCommandDescription,
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

            internal class ExecuteWriteFlagsCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteWriteFlagCommandName);

                public override string Description =>
                    Localization.GetString(Localization.Names.ExecuteWriteFlagCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 2)
                        return false;

                    if (args[1] != "0" && args[1] != "1")
                        return false;

                    var validFlags = new HashSet<string>(
                        Constants.Flags.Names,
                        StringComparer.InvariantCultureIgnoreCase);

                    return validFlags.Contains(args[0]);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    bool newValue = args[1] == "0" ? false : true;
                    switch (args[0].ToLowerInvariant())
                    {
                        case Constants.Flags.Zero:
                            items.Registers.FlagZ = newValue;
                            break;

                        case Constants.Flags.Subs:
                            items.Registers.FlagN = newValue;
                            break;

                        case Constants.Flags.Half:
                            items.Registers.FlagH = newValue;
                            break;

                        case Constants.Flags.Full:
                            items.Registers.FlagCY = newValue;
                            break;

                        default:
                            return;
                    }

                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.ExecuteWriteFlagCommandSuccessMessage,
                            args[0].ToLowerInvariant(),
                            newValue));
                }
            }

            internal class ExecuteResetAllCommand : FinalCommand
            {
                public override string Name => 
                    Localization.GetString(
                        Localization.Names.ExecuteResetAllRegistersCommandName);

                public override string Description =>
                    Localization.GetString(
                        Localization.Names.ExecuteResetAllRegistersCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 1)
                        return false;

                    return Name.StartsWith(
                        args[0],
                        StringComparison.InvariantCultureIgnoreCase);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    items.Registers.A = 0;
                    items.Registers.F = 0;
                    items.Registers.B = 0;
                    items.Registers.C = 0;
                    items.Registers.D = 0;
                    items.Registers.E = 0;
                    items.Registers.H = 0;
                    items.Registers.L = 0;
                    items.Registers.SP = 0;
                    items.Registers.PC = 256; // FIXME: move this to a constant.

                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.ExecuteResetAllRegistersSuccessMessage));
                }
            }
        }
    }
}
