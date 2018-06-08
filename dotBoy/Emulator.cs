using System;

using DotBoy.Core;
using DotBoy.Core.Instructions;
using DotBoy.Core.Logging;
using DotBoy.Interfaces;

namespace DotBoy
{
    public class Emulator
    {
        public static Emulator InitForRegularRun(
            Rom rom,
            ISleeper sleeper,
            bool trace = true,
            long cpuClockStep = 0)
        {
            return InitForInteractiveDebugging(
                rom, sleeper, trace, cpuClockStep,
                out _, out _, out _, out _, out _, out _, out _, out _);
        }

        public static Emulator InitForInteractiveDebugging(
            Rom rom,
            ISleeper sleeper,
            bool trace,
            long cpuClockStep,
            out IClock clock,
            out IChronometer chronometer,
            out IClockDivider clockDivider,
            out IClockObserver cpuClockObserver,
            out IMemory memory,
            out IRegisters registers,
            out IInstructionSet instructionSet,
            out IPipeline pipeline)
        {
            if (rom.Information.Type != CartridgeTypeId.RomOnly)
            {
                Console.Error.WriteLine("Unsupported cartridge type");
                Environment.Exit(1);
            }

            clock = new Clock();
            chronometer = new Chronometer(clock);
            clockDivider = ClockDivider.FromMillisPerStep(chronometer, cpuClockStep);

            memory = trace
                ? (IMemory)new LoggedMemory(new Memory())
                : new Memory();

            registers = trace
                ? (IRegisters)new LoggedRegisters(new Registers())
                : new Registers();

            instructionSet = trace
                ? (IInstructionSet)new LoggedInstructionSet(new InstructionSet())
                : new InstructionSet();

            pipeline = trace
                ? (IPipeline)new LoggedPipeline(new Pipeline(instructionSet))
                : new Pipeline(instructionSet);

            memory.BlockLoad(
                rom.Content,
                0,
                0,
                32 * 1024);

            registers.PC = 0x0100;

            cpuClockObserver = new Cpu(memory, registers, pipeline);
            clockDivider.AddObserver(trace ? new LoggedCpu(cpuClockObserver) : cpuClockObserver);

            return new Emulator(sleeper, chronometer, clockDivider);
        }

        Emulator(
            ISleeper sleeper,
            IChronometer chronometer,
            params IClockDivider[] clockDividers)
        {
            mSleeper = sleeper;
            mChronometer = chronometer;
            mClockDividers = clockDividers;
        }

        public void Run()
        {
            RunUntilCondition(() => false);
        }

        public void RunUntilCondition(Func<bool> condition)
        {
            mChronometer.Start();

            while (mChronometer.IsRunning)
            {
                mChronometer.Update();

                long msLeft = long.MaxValue;
                foreach (var clockDivider in mClockDividers)
                {
                    clockDivider.Trigger();

                    if (clockDivider.MsLeft < msLeft)
                        msLeft = clockDivider.MsLeft;
                }

                mSleeper.Sleep(Math.Max(0, msLeft));

                if (condition.Invoke())
                {
                    mChronometer.Update();
                    mChronometer.Stop();
                    return;
                }
            }
        }

        public void Pause()
        {
            mChronometer.Stop();
        }

        readonly ISleeper mSleeper;
        readonly IChronometer mChronometer;
        readonly IClockDivider[] mClockDividers;
    }
}
