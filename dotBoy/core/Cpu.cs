using System;

using DotBoy.Interfaces;

namespace DotBoy.Core
{
#warning Untested class
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

        void IClockObserver.OnClockTick()
        {
            byte instruction = mMemory[mRegisters.PC];
            mPipeline.DecodeAndExecute(instruction, mRegisters, mMemory);
        }

        readonly IMemory mMemory;
        readonly IRegisters mRegisters;
        readonly IPipeline mPipeline;
    }
}
