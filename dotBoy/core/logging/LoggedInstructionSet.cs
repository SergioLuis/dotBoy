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

        #region 8-bit Transfer and Input/Output Instructions
        void IInstructionSet.LdRR(byte instruction, IRegisters registers)
        {
            mLog.Trace("LD r,r'");
            mInstructionSet.LdRR(instruction, registers);
        }

        void IInstructionSet.LdRN(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD r, n");
            mInstructionSet.LdRN(instruction, registers, memory);
        }

        void IInstructionSet.LdHLN(IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD (HL), n");
            mInstructionSet.LdHLN(registers, memory);
        }

        void IInstructionSet.LdHLiA(IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD (HLI), A");
            mInstructionSet.LdHLiA(registers, memory);
        }

        void IInstructionSet.LdHLdA(IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD (HLD), A");
            mInstructionSet.LdHLdA(registers, memory);
        }
        #endregion

        #region 16-Bit Transfer Instructions
        void IInstructionSet.LdDdNn(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Trace("LD dd, nn");
            mInstructionSet.LdDdNn(instruction, registers, memory);
        }
        #endregion

        #region Jump instructions
        void IInstructionSet.JpNn(IRegisters registers, IMemory memory)
        {
            mLog.Trace("JP nn");
            mInstructionSet.JpNn(registers, memory);
        }
        #endregion

        #region 8-Bit Arithmetic and Logical Operation Instructions
        void IInstructionSet.XorR(byte instruction, IRegisters registers)
        {
            mLog.Trace("XOR r");
            mInstructionSet.XorR(instruction, registers);
        }

        void IInstructionSet.XorN(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Trace("XOR n");
            mInstructionSet.XorN(instruction, registers, memory);
        }

        void IInstructionSet.XorHL(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Trace("XOR (HL)");
            mInstructionSet.XorHL(instruction, registers, memory);
        }

        void IInstructionSet.DecR(byte instruction, IRegisters registers)
        {
            mLog.Trace("DEC r");
            mInstructionSet.DecR(instruction, registers);
        }

        void IInstructionSet.DecHL(IRegisters registers, IMemory memory)
        {
            mLog.Trace("DEC (HL)");
            mInstructionSet.DecHL(registers, memory);
        }
        #endregion

        #region General-Purpose Arithmetic Operations and CPU Control Instructions
        void IInstructionSet.Nop(IRegisters registers)
        {
            mLog.Trace("NOP");
            mInstructionSet.Nop(registers);
        }
        #endregion

        readonly IInstructionSet mInstructionSet;

        static readonly Logger mLog = LogManager.GetLogger("InstructionSet");
    }
}
