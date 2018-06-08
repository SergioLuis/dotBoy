using System.Collections.Generic;

using DotBoy;
using DotBoy.Interfaces;

using ConsoleRunner.Debugging.Breakpoints;

namespace ConsoleRunner.Debugging
{
    internal class DebuggingItems
    {
        internal IClock Clock { get; }
        internal IChronometer Chronometer { get; }
        internal IClockDivider ClockDivider { get; }
        internal IClockObserver ClockObserver { get; }
        internal IMemory Memory { get; }
        internal IRegisters Registers { get; }
        internal IInstructionSet InstructionSet { get; }
        internal IPipeline Pipeline { get; }

        internal Emulator Emulator { get; }

        internal List<BaseBreakpoint> Breakpoints { get; }

        internal DebuggingItems(
            IClock clock,
            IChronometer chronometer,
            IClockDivider clockDivider,
            IClockObserver clockObserver,
            IMemory memory,
            IRegisters registers,
            IInstructionSet instructionSet,
            IPipeline pipeline,
            Emulator emulator)
        {
            Clock = clock;
            Chronometer = chronometer;
            ClockDivider = clockDivider;
            ClockObserver = clockObserver;
            Memory = memory;
            Registers = registers;
            InstructionSet = instructionSet;
            Pipeline = pipeline;

            Emulator = emulator;

            Breakpoints = new List<BaseBreakpoint>();
        }
    }
}
