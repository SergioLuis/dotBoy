using DotBoy.Core;
using DotBoy.Core.Emulation;
using DotBoy.Core.Interfaces;

using DotBoy.Debugging.CommandLine;
using DotBoy.Debugging.CommandLine.Commands;

namespace DotBoy.Debugging
{
    public static class Debugger
    {
        public static void RunDebugSession(
            Rom rom,
            ISleeper sleeper,
            bool trace,
            bool realTime,
            long cpuClockStep)
        {
            var emulator = Emulator.InitForInteractiveDebugging(
                rom,
                sleeper,
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
