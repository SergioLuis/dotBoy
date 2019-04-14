using System;

using NLog;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation
{
    public class Cpu : IClockObserver
    {
        public Cpu(
            IMemory memory,
            IRegisters registers,
            IPipeline pipeline,
            bool realTime)
        {
            mMemory = memory;
            mRegisters = registers;
            mPipeline = pipeline;
        }

        bool IClockObserver.OnClockTick()
        {
            return ExecuteCycle();
        }

        bool ExecuteCycle()
        {
            if (mCycleCount > 0)
            {
                mCycleCount--;
                return true;
            }

            byte instruction = mMemory[mRegisters.PC];
            bool res = mPipeline.DecodeAndExecute(
                instruction, mRegisters, mMemory, out mCycleCount);

            mCycleCount = mRealTime ? 0 : mCycleCount - 1;
            return res;
        }

        readonly IMemory mMemory;
        readonly IRegisters mRegisters;
        readonly IPipeline mPipeline;
        readonly bool mRealTime;

        int mCycleCount;
    }
}
