﻿using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core.Instructions
{
    public static class NopInstr
    {
#warning untested method
        public static void ExecuteNop(IRegisters registers)
        {
            mLog.Info("NOP");
            mLog.Warn("NOP Not checking if interruptions are disabled.");
            registers.PC++;
        }

        static readonly Logger mLog = LogManager.GetLogger("Instructions");
    }
}
