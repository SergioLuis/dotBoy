using DotBoy;
using DotBoy.Interfaces;

using ConsoleRunner.Debugging.Commands;

namespace ConsoleRunner.Debugging
{
    internal static class Debugger
    {
        internal static void RunDebugSession(
            Rom rom, bool trace, bool realTime, long cpuClockStep)
        {
            var emulator = Emulator.InitForInteractiveDebugging(
                rom,
                new RealTimeSleeper(),
                trace,
                realTime,
                cpuClockStep,
                out IClock clock,
                out IChronometer chronometer,
                out IClockDivider clockDivider,
                out IClockObserver clockObserver,
                out IMemory memory,
                out IRegisters registers,
                out IInstructionSet instructionSet,
                out IPipeline pipeline);

            var items = new DebuggingItems(
                clock,
                chronometer,
                clockDivider,
                clockObserver,
                memory,
                registers,
                instructionSet,
                pipeline,
                emulator);

            ICommand dotBoyCommand = new DotBoyCommand();
            dotBoyCommand.ExecuteInteractive(items);
        }
    }
}
