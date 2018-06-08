using System;

using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Cpu : IClockObserver
    {
        public Cpu(
            IMemory memory,
            IRegisters registers,
            IPipeline pipeline)
        {
            mMemory = memory;
            mRegisters = registers;
            mPipeline = pipeline;
        }

        bool IClockObserver.OnClockTick()
        {
            byte instruction = mMemory[mRegisters.PC];
            return mPipeline.DecodeAndExecute(instruction, mRegisters, mMemory);
        }

        readonly IMemory mMemory;
        readonly IRegisters mRegisters;
        readonly IPipeline mPipeline;
    }
}
