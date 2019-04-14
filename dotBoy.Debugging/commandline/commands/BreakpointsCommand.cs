using System;
using System.Collections.Generic;
using System.Linq;

using DotBoy.Debugging.Breakpoints;
using DotBoy.Debugging.Conditions;
using DotBoy.Debugging.CommandLine;

namespace DotBoy.Debugging.CommandLine.Commands
{
    internal class BreakpointsCommand : InteractiveCommand
    {
        public override string Name =>
            Localization.GetString(Localization.Names.BreakpointsCommandName);

        public override string Description =>
            Localization.GetString(Localization.Names.BreakpointsCommandDescription);

        public override string Prompt =>
            Localization.GetString(Localization.Names.BreakpointsCommandPrompt);

        internal BreakpointsCommand()
        {
            mSubcommands.Add(new ExecuteListBreakpointsCommand());

            mSubcommands.Add(new AddBreakpointCommand());
            mSubcommands.Add(new EnableBreakpointCommand());
            mSubcommands.Add(new DisableBreakpointCommand());
            mSubcommands.Add(new RemoveBreakpointCommand());

            mSubcommands.Add(new ExecuteRunCommand());
        }

        internal class ExecuteListBreakpointsCommand : FinalCommand
        {
            public override string Name =>
                Localization.GetString(
                    Localization.Names.ExecuteListBreakpointsCommandName);

            public override string Description =>
                Localization.GetString(
                    Localization.Names.ExecuteListBreakpointsCommandDescription);

            public override bool CanExecute(string[] command, DebuggingItems items)
            {
                if (command.Length != 1)
                    return false;

                return Name.StartsWith(
                    command[0],
                    StringComparison.InvariantCultureIgnoreCase);
            }

            public override void Execute(string[] args, DebuggingItems items)
            {
                if (items.Breakpoints.Count == 0)
                {
                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.ExecuteListBreakpointsCommandNoBreakpointsErrorMessage));
                    return;
                }

                var conditionsAndDescriptions = new List<Tuple<string, string>>();

                int maxConditionLength = int.MinValue;
                foreach (var breakpoint in items.Breakpoints)
                {
                    conditionsAndDescriptions.Add(
                        new Tuple<string, string>(
                            breakpoint.Condition,
                            breakpoint.Description));

                    if (breakpoint.Condition.Length > maxConditionLength)
                        maxConditionLength = breakpoint.Condition.Length;
                }

                for (int i = 0; i < conditionsAndDescriptions.Count; i++)
                {
                    var t = conditionsAndDescriptions[i];
                    string status = items.Breakpoints[i].Enabled ? "[O]" : "[ ]";

                    Console.WriteLine(
                        $" {i}: {status} ({t.Item1.PadLeft(maxConditionLength)}) ==> {t.Item2}");
                }
            }
        }

        internal class AddBreakpointCommand : InteractiveCommand
        {
            public override string Name =>
                Localization.GetString(
                    Localization.Names.AddBreakpointCommandName);

            public override string Description =>
                Localization.GetString(
                    Localization.Names.AddBreakpointCommandDescription);

            public override string Prompt =>
                Localization.GetString(
                    Localization.Names.AddBreakpointCommandPrompt);

            internal AddBreakpointCommand()
            {
                mSubcommands.Add(new AddBreakpointByRegisterValueCommand());
            }

            internal class AddBreakpointByRegisterValueCommand : InteractiveCommand
            {
                public override string Name =>
                    Localization.GetString(
                        Localization.Names.AddBreakpointByRegisterValueCommandName);

                public override string Description =>
                    Localization.GetString(
                        Localization.Names.AddBreakpointByRegisterValueCommandDescription);

                public override string Prompt =>
                    Localization.GetString(
                        Localization.Names.AddBreakpointByRegisterValueCommandPrompt);

                internal AddBreakpointByRegisterValueCommand()
                {
                    mSubcommands.Add(new ExecuteAddByRegisterValueCommand());
                }

                internal class ExecuteAddByRegisterValueCommand : FinalCommand
                {
                    public override string Name =>
                        Localization.GetString(
                            Localization.Names.ExecuteAddBreakpointByRegisterValueCommandName);

                    public override string Description =>
                        Localization.GetString(
                            Localization.Names.ExecuteAddBreakpointByRegisterValueCommandDescription);

                    public override bool CanExecute(string[] args, DebuggingItems items)
                    {
                        if (args.Length != 2)
                            return false;

                        return ushort.TryParse(args[1], out _);
                    }

                    public override void Execute(string[] args, DebuggingItems items)
                    {
                        if (!RegisterValueBreakpoint.TryCreate(
                            args[0], ushort.Parse(args[1]), items, out var breakpoint))
                        {
                            ConsoleIO.WriteLine(
                                Localization.GetString(
                                    Localization.Names.ExecuteAddBreakpointByRegisterValueCommandFailureMessage));
                            return;
                        }

                        items.Breakpoints.Add(breakpoint);
                        ConsoleIO.WriteLine(
                            Localization.GetString(
                                Localization.Names.ExecuteAddBreakpointByRegisterValueCommandSuccessMessage));
                    }
                }
            }
        }

        internal class EnableBreakpointCommand : InteractiveCommand
        {
            public override string Name =>
                Localization.GetString(
                    Localization.Names.EnableBreakpointCommandName);

            public override string Description =>
                Localization.GetString(
                    Localization.Names.EnableBreakpointCommandDescription);

            public override string Prompt =>
                Localization.GetString(
                    Localization.Names.EnableBreakpointCommandPrompt);

            internal EnableBreakpointCommand()
            {
                mSubcommands.Add(new ExecuteEnableCommand());
            }

            internal class ExecuteEnableCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(
                        Localization.Names.ExecuteEnableBreakpointCommandName);

                public override string Description =>
                    Localization.GetString(
                        Localization.Names.ExecuteEnableBreakpointCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 1)
                        return false;

                    return int.TryParse(args[0], out _);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    int breakpointIndex = int.Parse(args[0]);
                    if (breakpointIndex >= items.Breakpoints.Count)
                    {
                        ConsoleIO.WriteLine(
                            Localization.GetString(
                                Localization.Names.ExecuteEnableBreakpointCommandFailureMessage,
                                breakpointIndex));
                        return;
                    }

                    items.Breakpoints[breakpointIndex].Enabled = true;
                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.ExecuteEnableBreakpointCommandSuccessMessage,
                            breakpointIndex));
                }
            }
        }

        internal class DisableBreakpointCommand : InteractiveCommand
        {
            public override string Name =>
                Localization.GetString(
                    Localization.Names.DisableBreakpointCommandName);

            public override string Description =>
                Localization.GetString(
                    Localization.Names.DisableBreakpointCommandDescription);

            public override string Prompt =>
                Localization.GetString(
                    Localization.Names.DisableBreakpointCommandPrompt);

            internal DisableBreakpointCommand()
            {
                mSubcommands.Add(new ExecuteDisableCommand());
            }

            internal class ExecuteDisableCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(
                        Localization.Names.ExecuteDisableBreakpointCommandName);

                public override string Description =>
                    Localization.GetString(
                        Localization.Names.ExecuteDisableBreakpointCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 1)
                        return false;

                    return int.TryParse(args[0], out _);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    int breakpointIndex = int.Parse(args[0]);
                    if (breakpointIndex >= items.Breakpoints.Count)
                    {
                        ConsoleIO.WriteLine(
                            Localization.GetString(
                                Localization.Names.ExecuteDisableBreakpointCommandFailureMessage,
                                breakpointIndex));
                        return;
                    }

                    items.Breakpoints[breakpointIndex].Enabled = false;
                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.ExecuteDisableBreakpointCommandSuccessMessage,
                            breakpointIndex));
                }
            }
        }

        internal class RemoveBreakpointCommand : InteractiveCommand
        {
            public override string Name =>
                Localization.GetString(
                    Localization.Names.RemoveBreakpointCommandName);

            public override string Description =>
                Localization.GetString(
                    Localization.Names.RemoveBreakpointCommandDescription);

            public override string Prompt =>
                Localization.GetString(
                    Localization.Names.RemoveBreakpointCommandPrompt);

            internal RemoveBreakpointCommand()
            {
                mSubcommands.Add(new ExecuteRemoveCommand());
            }

            internal class ExecuteRemoveCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(
                        Localization.Names.ExecuteRemoveBreakpointCommandName);

                public override string Description =>
                    Localization.GetString(
                        Localization.Names.ExecuteRemoveBreakpointCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 1)
                        return false;

                    return int.TryParse(args[0], out _);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    int breakpointIndex = int.Parse(args[0]);

                    if (breakpointIndex >= items.Breakpoints.Count)
                    {
                        ConsoleIO.WriteLine(
                            Localization.GetString(
                                Localization.Names.ExecuteRemoveBreakpointCommandFailureMessage,
                                breakpointIndex));
                        return;
                    }

                    items.Breakpoints.RemoveAt(breakpointIndex);
                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.ExecuteRemoveBreakpointCommandSuccessMessage,
                            breakpointIndex));
                }
            }
        }

        internal class ExecuteRunCommand : FinalCommand
        {
            public override string Name =>
                Localization.GetString(
                    Localization.Names.ExecuteRunUntilBreakpointCommandName);

            public override string Description =>
                Localization.GetString(
                    Localization.Names.ExecuteRunUntilBreakpointCommandDescription);

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
                if (items.Breakpoints.Count == 0)
                {
                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.ExecuteRunUntilBreakpointFailureMessage));
                    return;
                }

                if (items.Breakpoints.FirstOrDefault(br => br.Enabled) == null)
                {
                    ConsoleIO.WriteLine(
                        Localization.GetString(
                            Localization.Names.ExecuteRunUntilBreakpointFailureMessage));
                    return;
                }

                var condition = new BreakpointCondition(items.Breakpoints);

                items.Emulator.RunUntilCondition(
                    ((ICondition)condition).IsConditionMet);

                ConsoleIO.WriteLine(
                    Localization.GetString(
                        Localization.Names.ExecuteRunUntilBreakpointSuccessMessage,
                        condition.TriggeredBreakpoint.Condition));
            }
        }
    }
}
