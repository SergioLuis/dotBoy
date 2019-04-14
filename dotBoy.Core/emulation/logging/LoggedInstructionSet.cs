using System;

using NLog;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation.Logging
{
    public class LoggedInstructionSet : IInstructionSet
    {
        public LoggedInstructionSet(IInstructionSet instructionSet)
        {
            mInstructionSet = instructionSet;
        }

        #region 8-bit Transfer and Input/Output Instructions
        int IInstructionSet.LdRR(byte instruction, IRegisters registers)
        {
            mLog.Trace("LD r,r'");
            return mInstructionSet.LdRR(instruction, registers);
        }

        int IInstructionSet.LdRN(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD r, n");
            return mInstructionSet.LdRN(instruction, registers, memory);
        }

        int IInstructionSet.LdHLN(IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD (HL), n");
            return mInstructionSet.LdHLN(registers, memory);
        }

        int IInstructionSet.LdHLiA(IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD (HLI), A");
            return mInstructionSet.LdHLiA(registers, memory);
        }

        int IInstructionSet.LdHLdA(IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD (HLD), A");
            return mInstructionSet.LdHLdA(registers, memory);
        }
        #endregion

        #region 16-Bit Transfer Instructions
        int IInstructionSet.LdDdNn(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD dd, nn");
            return mInstructionSet.LdDdNn(instruction, registers, memory);
        }
        #endregion

        #region Jump instructions
        int IInstructionSet.JpNn(IRegisters registers, IMemory memory)
        {
            mLog.Trace("JP nn");
            return mInstructionSet.JpNn(registers, memory);
        }

        int IInstructionSet.JrCcE(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Trace("JR cc, e");
            return mInstructionSet.JrCcE(instruction, registers, memory);
        }
        #endregion

        #region 8-Bit Arithmetic and Logical Operation Instructions
        int IInstructionSet.XorR(byte instruction, IRegisters registers)
        {
            mLog.Trace("XOR r");
            return mInstructionSet.XorR(instruction, registers);
        }

        int IInstructionSet.XorN(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Trace("XOR n");
            return mInstructionSet.XorN(instruction, registers, memory);
        }

        int IInstructionSet.XorHL(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Trace("XOR (HL)");
            return mInstructionSet.XorHL(instruction, registers, memory);
        }

        int IInstructionSet.DecR(byte instruction, IRegisters registers)
        {
            mLog.Trace("DEC r");
            return mInstructionSet.DecR(instruction, registers);
        }

        int IInstructionSet.DecHL(IRegisters registers, IMemory memory)
        {
            mLog.Trace("DEC (HL)");
            return mInstructionSet.DecHL(registers, memory);
        }
        #endregion

        #region General-Purpose Arithmetic Operations and CPU Control Instructions
        int IInstructionSet.Nop(IRegisters registers)
        {
            mLog.Trace("NOP");
            return mInstructionSet.Nop(registers);
        }
        #endregion

        readonly IInstructionSet mInstructionSet;

        static readonly Logger mLog = LogManager.GetLogger("InstructionSet");
    }
}
