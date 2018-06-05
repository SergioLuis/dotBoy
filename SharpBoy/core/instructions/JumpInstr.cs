using System;

using NLog;

using SharpBoy.Interfaces;

namespace SharpBoy.Core.Instructions
{
    public static class JumpInstr
    {
        public static void Execute_Jp_nn(IRegisters registers, IMemory memory)
        {
            mLog.Info("JP nn");

            registers.PC++;
            byte l = memory[registers.PC];

            registers.PC++;
            byte h = memory[registers.PC];

            registers.PC = BitConverter.ToUInt16(new byte[] { l, h }, 0);
        }
        
        static readonly Logger mLog = LogManager.GetLogger("Instructions");
    }
}
