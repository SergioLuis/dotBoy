using System;

using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core.Logging
{
    public class LoggedInstructionSet : IInstructionSet
    {
        public LoggedInstructionSet(IInstructionSet instructionSet)
        {
            mInstructionSet = instructionSet;
        }

        void IInstructionSet.JpNn(IRegisters registers, IMemory memory)
        {
            mLog.Info("JP nn");
            mInstructionSet.JpNn(registers, memory);
        }

        void IInstructionSet.LdRR(byte instruction, IRegisters registers)
        {
            mLog.Info("LD r,r'");
            mInstructionSet.LdRR(instruction, registers);
        }

        void IInstructionSet.Nop(IRegisters registers)
        {
            mLog.Info("NOP");
            mInstructionSet.Nop(registers);
        }

        readonly IInstructionSet mInstructionSet;

        static readonly Logger mLog = LogManager.GetLogger("InstructionSet");
    }
}
