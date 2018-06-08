using System;

using NLog;

using DotBoy.Core.Instructions;
using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Pipeline : IPipeline
    {
        public Pipeline(IInstructionSet instructionSet)
        {
            mInstructionSet = instructionSet;
        }

        void IPipeline.DecodeAndExecute(
            byte instruction, IRegisters registers, IMemory memory)
        {
            switch (instruction)
            {
                case 0x00:
                    mInstructionSet.Nop(registers);
                    return;

                #region 8-bit Transfer and Input/Output Instructions
                case 0x7F:
                case 0x78:
                case 0x79:
                case 0x7A:
                case 0x7B:
                case 0x7C:
                case 0x7D:
                case 0x47:
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 0x4F:
                case 0x48:
                case 0x49:
                case 0x4A:
                case 0x4B:
                case 0x4C:
                case 0x4D:
                case 0x57:
                case 0x50:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x5F:
                case 0x58:
                case 0x59:
                case 0x5A:
                case 0x5B:
                case 0x5C:
                case 0x5D:
                case 0x67:
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 0x64:
                case 0x65:
                case 0x6F:
                case 0x68:
                case 0x69:
                case 0x6A:
                case 0x6B:
                case 0x6C:
                case 0x6D:
                    mInstructionSet.LdRR(instruction, registers);
                    return;

                case 0x3E:
                case 0x06:
                case 0x0E:
                case 0x16:
                case 0x1E:
                case 0x26:
                case 0x2E:
                    mInstructionSet.LdRN(instruction, registers, memory);
                    return;

                case 0x36:
                    mInstructionSet.LdHLN(registers, memory);
                    return;

                case 0x22:
                    mInstructionSet.LdHLiA(registers, memory);
                    return;

                case 0x32:
                    mInstructionSet.LdHLdA(registers, memory);
                    return;
                #endregion

                #region 16-Bit Transfer Instructions
                case 0x01:
                case 0x11:
                case 0x21:
                case 0x31:
                    mInstructionSet.LdDdNn(instruction, registers, memory);
                    return;
                #endregion

                #region  Jump instructions
                case 0xC3:
                    mInstructionSet.JpNn(registers, memory);
                    return;
                #endregion

                #region 8-Bit Arithmetic and Logical Operation Instructions
                case 0xAF:
                case 0xA8:
                case 0xA9:
                case 0xAA:
                case 0xAB:
                case 0xAC:
                case 0xAD:
                    mInstructionSet.XorR(instruction, registers);
                    return;

                case 0xDE:
                    mInstructionSet.XorN(instruction, registers, memory);
                    return;

                case 0xAE:
                    mInstructionSet.XorHL(instruction, registers, memory);
                    return;

                case 0x3D:
                case 0x05:
                case 0x0D:
                case 0x15:
                case 0x1D:
                case 0x25:
                case 0x2D:
                    mInstructionSet.DecR(instruction, registers);
                    return;

                case 0x35:
                    mInstructionSet.DecHL(registers, memory);
                    return;
                #endregion

                default:
                    HaltAndCatchFire(instruction);
                    break;
            }
        }

        void HaltAndCatchFire(byte instruction)
        {
            mLog.Error("Instruction not implemented.");
            throw new NotImplementedException();
        }

        readonly IInstructionSet mInstructionSet;

        static readonly Logger mLog = LogManager.GetLogger("Pipeline");
    }
}
