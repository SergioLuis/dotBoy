using System;

using DotBoy.Core;
using DotBoy.Core.Instructions;
using DotBoy.Core.Logging;
using DotBoy.Interfaces;

namespace DotBoy
{
    public class Emulator
    {
        public static Emulator Init(
            Rom rom,
            ISleeper sleeper,
            bool trace = true,
            long cpuCloclStep = 0)
        {
            if (rom.Information.Type != CartridgeTypeId.RomOnly)
            {
                Console.Error.WriteLine("Unsupported cartridge type");
                Environment.Exit(1);
            }

            var clock = new Clock();
            var chronometer = new Chronometer(clock);
            IClockDivider cpuClockDivider =
                ClockDivider.FromMillisPerStep(chronometer, cpuCloclStep);

            IMemory memory = trace
                ? (IMemory)new LoggedMemory(new Memory())
                : new Memory();

            IRegisters registers = trace
                ? (IRegisters)new LoggedRegisters(new Registers())
                : new Registers();

            IInstructionSet instructionSet = trace
                ? (IInstructionSet)new LoggedInstructionSet(new InstructionSet())
                : new InstructionSet();

            IPipeline pipeline = trace
                ? (IPipeline)new LoggedPipeline(new Pipeline(instructionSet))
                : new Pipeline(instructionSet);

            memory.BlockLoad(
                rom.Content,
                0,
                0,
                32 * 1024);

            registers.PC = 0x0100;

            IClockObserver cpu = new Cpu(memory, registers, pipeline);
            cpuClockDivider.AddObserver(trace ? new LoggedCpu(cpu) : cpu);

            return new Emulator(sleeper, chronometer, cpuClockDivider);
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
