using System;

using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Cpu : ICpu
    {
        public Cpu(
            IClock clock,
            IMemory memory,
            IRegisters registers,
            IPipeline pipeline)
        {
            mClock = clock;
            mMemory = memory;
            mRegisters = registers;
            mPipeline = pipeline;
        }

        public void StartSynchronousExecution()
        {
            ExecuteInLoop();
        }

        public void StartAsynchronousExecution()
        {

        }

        void ExecuteInLoop()
        {
            while (true)
            {
                mClock.WaitUntilNextCycle();

                byte instruction = mMemory[mRegisters.PC];
                mPipeline.DecodeAndExecute(instruction, mRegisters, mMemory);
            }
        }

        readonly IClock mClock;
        readonly IMemory mMemory;
        readonly IRegisters mRegisters;
        readonly IPipeline mPipeline;
    }
}
