namespace ConsoleRunner.Debugging.Commands
{
    internal class DotBoyCommand : InteractiveCommand
    {
        public override string Name => "dotBoy";

        public override string Description => "Allows running debug commands.";

        public override string Prompt => "dotBoy";

        internal DotBoyCommand()
        {
            mSubcommands.Add(new RegistersCommand());
            mSubcommands.Add(new MemoryCommand());
            mSubcommands.Add(new ClockCommand());
            mSubcommands.Add(new BreakpointsCommand());
        }
    }
}
