using System;

using DotBoy.Core;
using DotBoy.Core.Logging;
using DotBoy.Interfaces;

namespace DotBoy
{
    public class Emulator
    {
        public static Emulator Init(Rom rom, ISleeper sleeper)
        {
            if (rom.Information.Type != CartridgeTypeId.RomOnly)
            {
                Console.Error.WriteLine("Unsupported cartridge type");
                Environment.Exit(1);
            }

            var clock = new Clock();
            var chronometer = new Chronometer(clock);
            IClockDivider cpuClockDivider =
                ClockDivider.FromMillisPerStep(chronometer, 0);

            IMemory memory = new LoggedMemory(new Memory());
            IRegisters registers = new LoggedRegisters(new Registers());
            IPipeline pipeline = new Pipeline();

            memory.BlockLoad(
                rom.Content,
                0,
                0,
                32 * 1024);

            registers.PC = 0x0100;

            var cpu = new Cpu(memory, registers, pipeline);

            cpuClockDivider.AddObserver(cpu);

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

                long minMsLeft = long.MaxValue;
                foreach (var clockDivider in mClockDividers)
                {
                    clockDivider.Trigger();

                    if (clockDivider.MsLeft < minMsLeft)
                        minMsLeft = clockDivider.MsLeft;
                }

                mSleeper.Sleep(Math.Max(0, minMsLeft));
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
