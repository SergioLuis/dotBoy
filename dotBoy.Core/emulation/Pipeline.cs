using System;

using NLog;

using DotBoy.Core.Emulation.Instructions;
using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation
{
    public class Pipeline : IPipeline
    {
        public Pipeline(IInstructionSet instructionSet)
        {
            mInstructionSet = instructionSet;
        }

        bool IPipeline.DecodeAndExecute(
            byte instruction,
            IRegisters registers,
            IMemory memory,
            out int consumedCycles)
        {
            bool succeeded = true;
            switch (instruction)
            {
                case 0x00:
                    consumedCycles =
                        mInstructionSet.Nop(registers);
                    break;

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
                    consumedCycles =
                        mInstructionSet.LdRR(instruction, registers);
                    break;

                case 0x3E:
                case 0x06:
                case 0x0E:
                case 0x16:
                case 0x1E:
                case 0x26:
                case 0x2E:
                    consumedCycles =
                        mInstructionSet.LdRN(instruction, registers, memory);
                    break;

                case 0x36:
                    consumedCycles =
                        mInstructionSet.LdHLN(registers, memory);
                    break;

                case 0x22:
                    consumedCycles =
                        mInstructionSet.LdHLiA(registers, memory);
                    break;

                case 0x32:
                    consumedCycles =
                        mInstructionSet.LdHLdA(registers, memory);
                    break;
                #endregion

                #region 16-Bit Transfer Instructions
                case 0x01:
                case 0x11:
                case 0x21:
                case 0x31:
                    consumedCycles =
                        mInstructionSet.LdDdNn(instruction, registers, memory);
                    break;
                #endregion

                #region  Jump instructions
                case 0xC3:
                    consumedCycles =
                        mInstructionSet.JpNn(registers, memory);
                    break;

                case 0x20:
                case 0x28:
                case 0x30:
                case 0x38:
                    consumedCycles =
                        mInstructionSet.JrCcE(instruction, registers, memory);
                    break;
                #endregion

                #region 8-Bit Arithmetic and Logical Operation Instructions
                case 0xAF:
                case 0xA8:
                case 0xA9:
                case 0xAA:
                case 0xAB:
                case 0xAC:
                case 0xAD:
                    consumedCycles =
                        mInstructionSet.XorR(instruction, registers);
                    break;

                case 0xDE:
                    consumedCycles =
                        mInstructionSet.XorN(instruction, registers, memory);
                    break;

                case 0xAE:
                    consumedCycles =
                        mInstructionSet.XorHL(instruction, registers, memory);
                    break;

                case 0x3D:
                case 0x05:
                case 0x0D:
                case 0x15:
                case 0x1D:
                case 0x25:
                case 0x2D:
                    consumedCycles =
                        mInstructionSet.DecR(instruction, registers);
                    break;

                case 0x35:
                    consumedCycles =
                        mInstructionSet.DecHL(registers, memory);
                    break;
                #endregion

                default:
                    HaltAndCatchFire(instruction, registers.PC);
                    succeeded = false;
                    consumedCycles = 1;
                    break;
            }

            return succeeded;
        }

        void HaltAndCatchFire(byte instruction, ushort programCounter)
        {
            string messageFormat =
                $"Instruction not implemented.{Environment.NewLine}" +
                $"    Instruction: {{0,6}} / {{1,4}} / {{2,16}}{Environment.NewLine}" +
                $"        Address: {{3,6}} / {{4,4}} / {{5,16}}{Environment.NewLine}";

            string message = string.Format(
                messageFormat,
                string.Format("0x{0:X2}", instruction),
                instruction,
                Convert.ToString(instruction, 2).PadLeft(8, '0'),
                string.Format("0x{0:X4}", programCounter),
                programCounter,
                Convert.ToString(instruction, 2).PadLeft(16, '0'));

            mLog.Error(message);
        }

        readonly IInstructionSet mInstructionSet;

        static readonly Logger mLog = LogManager.GetLogger("Pipeline");
    }
}
