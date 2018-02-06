using System;
using System.Collections.Generic;
using System.Text;

using SharpBoy.Interfaces;

namespace SharpBoy.Core
{
    public class Cpu : ICpu
    {
        public Cpu(IClock clock, IMemory memory, IRegisters registers)
        {
            mClock = clock;
            mMemory = memory;
            mRegisters = registers;
        }

        public void StartSyncrhonousExecution()
        {

        }

        public void StartAsynchronousExecution()
        {

        }

        void ExecuteInLoop()
        {
            while (true)
            {
                mClock.WaitUntilNextCycle();
                ExecuteNext();
            }
        }

        void ExecuteNext()
        {
            // TODO read instruction using PC

            // TODO decode instruction

            // TODO execute instruction

            // TODO update PC
        }

        void ICpu.StartExecution()
        {
            throw new NotImplementedException();
        }

        readonly IClock mClock;
        readonly IMemory mMemory;
        readonly IRegisters mRegisters;
    }
}
