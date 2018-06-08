using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRunner.Debugging.Commands
{
    internal class DotBoyCommand : InteractiveCommand
    {
        public override string Name => "dotBoy";

        public override string Description => "Allows running debug commands.";

        public override string Prompt => "dotBoy";

        internal DotBoyCommand()
        {
            mSubcommands = new CommandCollection();
            mSubcommands.AddCommand(new RegistersCommand());
            mSubcommands.AddCommand(new ClockCommand());
            mSubcommands.AddCommand(new BreakpointsCommand());
        }
    }
}
