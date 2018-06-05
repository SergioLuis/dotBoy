using System;

using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core.Instructions
{
    public static class LoadInstr
    {
#warning Untested method
        public static void Execute_Ld_r_r(byte instruction, IRegisters registers)
        {
            mLog.Info("LD r,r'");

            byte destination = (byte)(instruction & 0x38);
            byte source = (byte)(instruction & 0x07);

            throw new NotImplementedException();
        }

        static readonly Logger mLog = LogManager.GetLogger("Instructions");
    }
}
