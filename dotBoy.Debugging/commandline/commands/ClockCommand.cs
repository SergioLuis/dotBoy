using DotBoy.Debugging.Conditions;

namespace DotBoy.Debugging.CommandLine.Commands
{
    internal class ClockCommand : InteractiveCommand
    {
        public override string Name =>
            Localization.GetString(Localization.Names.ClockCommandName);

        public override string Description =>
            Localization.GetString(Localization.Names.ClockCommandDescription);

        public override string Prompt =>
            Localization.GetString(Localization.Names.ClockCommandPrompt);

        internal ClockCommand()
        {
            mSubcommands.Add(new ClockTicksCommand());
        }

        internal class ClockTicksCommand : InteractiveCommand
        {
            public override string Name =>
                Localization.GetString(Localization.Names.ClockTicksCommandName);

            public override string Description =>
                Localization.GetString(Localization.Names.ClockTicksCommandDescription);

            public override string Prompt => 
                Localization.GetString(Localization.Names.ClockTicksCommandPrompt);

            internal ClockTicksCommand()
            {
                mSubcommands.Add(new ExecuteTicksCommand());
            }

            internal class ExecuteTicksCommand : FinalCommand
            {
                public override string Name =>
                    Localization.GetString(Localization.Names.ExecuteTicksCommandName);

                public override string Description =>
                    Localization.GetString(Localization.Names.ExecuteTicksCommandDescription);

                public override bool CanExecute(string[] args, DebuggingItems items)
                {
                    if (args.Length != 1)
                        return false;

                    return int.TryParse(args[0], out _);
                }

                public override void Execute(string[] args, DebuggingItems items)
                {
                    long currentTicks = items.Chronometer.TimesUpdated;
                    long targetTicks = currentTicks + int.Parse(args[0]);

                    ICondition condition = new ClockTicksCondition(
                        items.Chronometer.TimesUpdated + int.Parse(args[0]),
                        items.Chronometer);

                    items.Emulator.RunUntilCondition(condition.IsConditionMet);
                }
            }
        }
    }
}
