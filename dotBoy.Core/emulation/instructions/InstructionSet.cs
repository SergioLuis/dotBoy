using System;

using NLog;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation.Instructions
{
    public class InstructionSet : IInstructionSet
    {
        #region 8-bit Transfer and Input/Output Instructions
        /// <summary>
        /// LD r, r' | r := r'
        /// 
        /// Loads the contents of register 'r'' into register 'r'.
        /// 
        /// Affected flags: none
        /// 
        /// Cycles: 1
        /// </summary>
        public int LdRR(byte instruction, IRegisters registers)
        {
#warning Untested method
            int rSrc = instruction & 0b111;
            int rDst = instruction >> 3 & 0b111;

            registers.Copy(rSrc, rDst);

            registers.PC++;

            return InstructionCycles.LdRR;
        }

        /// <summary>
        /// LD r, n | r := n
        /// 
        /// Lods 8-bit inmediate data 'n' into register 'r'.
        /// 
        /// Affected flags: none
        /// 
        /// Cycles: 2
        /// </summary>
        public int LdRN(byte instruction, IRegisters registers, IMemory memory)
        {
#warning Untested method
            int rDst = instruction >> 3 & 0b111;

            registers.Write(rDst, memory[++registers.PC]);

            registers.PC++;

            return InstructionCycles.LdRN;
        }

        /// <summary>
        /// LD (HL), n | (HL) := n
        /// 
        /// Loads 8-bit inmediate data 'n' into memory specified by register
        /// pair (HL).
        /// 
        /// Affected flags: none
        /// 
        /// Cycles: 3
        /// </summary>
        public int LdHLN(IRegisters registers, IMemory memory)
        {
#warning Untested method
            memory[registers.HL] = memory[++registers.PC];

            registers.PC++;

            return InstructionCycles.LdHLN;
        }

        /// <summary>
        /// LD (HLI), A | (HL) := A; HL := HL + 1
        /// 
        /// Stores the contents of register A in the memory specified by register
        /// pair HL and simultaneously increments the contents of HL.
        /// 
        /// Affected flags: none
        /// 
        /// Cycles: 2
        /// </summary>
        public int LdHLiA(IRegisters registers, IMemory memory)
        {
#warning Untested method
            memory[registers.HL++] = registers.A;

            registers.PC++;

            return InstructionCycles.LdHLiA;
        }

        /// <summary>
        /// LD (HLD), A | (HL) := A; HL := HL - 1
        /// 
        /// Stores the contents of register A in the memory specified by register
        /// pair HL and simultaneously decrements the contents of HL.
        /// 
        /// Affected flags: none
        /// 
        /// Cycles: 2
        /// </summary>
        public int LdHLdA(IRegisters registers, IMemory memory)
        {
#warning Untested method
            memory[registers.HL--] = registers.A;

            registers.PC++;

            return InstructionCycles.LdHLdA;
        }
        #endregion

        #region 16-Bit Transfer Instructions
        /// <summary>
        /// LD dd, nn | dd := nn
        /// 
        /// Loads 2 bytes of inmediate data to register pair 'dd'.
        /// 
        /// 'dd' codes are as follows:
        ///     00: BC
        ///     01: DE
        ///     10: HL
        ///     11: SP
        /// 
        /// Affected flags: none
        /// 
        /// Cycles: 3
        /// </summary>
        public int LdDdNn(byte instruction, IRegisters registers, IMemory memory)
        {
#warning Untested method
            byte lAddr = memory[++registers.PC];
            byte hAddr = memory[++registers.PC];

            int r = instruction >> 4 & 0b11;

            switch (r)
            {
                case (0b00):
                    registers.B = hAddr;
                    registers.C = lAddr;
                    break;

                case (0b01):
                    registers.D = hAddr;
                    registers.E = lAddr;
                    break;

                case (0b10):
                    registers.H = hAddr;
                    registers.L = lAddr;
                    break;

                case (0b11):
                    registers.SP = (ushort)(hAddr << 4 | lAddr);
                    break;
            }

            registers.PC++;

            return InstructionCycles.LdDdNn;
        }
        #endregion

        #region 8-Bit Arithmetic and Logical Operation Instructions
        /// <summary>
        /// XOR r | A := A xor r
        /// 
        /// Takes the logical exclusive-OR for each bit of the contents of
        /// register 'r' and register 'A', and stores the result in register 'A'.
        /// 
        /// Affected flags:
        ///     CY: reset
        ///      H: reset
        ///      N: reset
        ///      Z: '1' if 'A' is set to zero, '0' otherwise.
        /// 
        /// Cycles: 1
        /// </summary>
        public int XorR(byte instruction, IRegisters registers)
        {
#warning Untested method
            int r = instruction & 0b111;

            byte result = (byte)(registers.A ^ registers.Read(r));

            registers.A = result;

            registers.FlagCY = false;
            registers.FlagH = false;
            registers.FlagN = false;
            registers.FlagZ = result == 0;

            registers.PC++;

            return InstructionCycles.XorR;
        }

        /// <summary>
        /// XOR n | A := A xor n
        /// 
        /// Takes the logical exclusive-OR for each bit of the contents of the
        /// inmediate 8 bit data 'n' and register 'A', and stores the result in
        /// register 'A'.
        /// 
        /// Affected flags:
        ///     CY: reset
        ///      H: reset
        ///      N: reset
        ///      Z: '1' if 'A' is set to zero, '0' otherwise.
        /// 
        /// Cycles: 2
        /// </summary>
        public int XorN(byte instruction, IRegisters registers, IMemory memory)
        {
#warning Untested method
            byte result = (byte)(registers.A ^ memory[++registers.PC]);

            registers.A = result;

            registers.FlagCY = false;
            registers.FlagH = false;
            registers.FlagN = false;
            registers.FlagZ = result == 0;

            registers.PC++;

            return InstructionCycles.XorN;
        }

        /// <summary>
        /// XOR (HL) | A := A xor (HL)
        /// 
        /// Takes the logical exclusive-OR for each bit of the contents of
        /// a 16-bit address stored at (HL) and register 'A', and stores the
        /// result in register 'A'.
        /// 
        /// Affected flags:
        ///     CY: reset
        ///      H: reset
        ///      N: reset
        ///      Z: '1' if 'A' is set to zero, '0' otherwise.
        /// 
        /// Cycles: 2
        /// </summary>
        public int XorHL(byte instruction, IRegisters registers, IMemory memory)
        {
#warning Untested method
            ushort address = memory[registers.HL];
            byte result = (byte)(registers.A ^ memory[address]);

            registers.A = result;

            registers.FlagCY = false;
            registers.FlagH = false;
            registers.FlagN = false;
            registers.FlagZ = result == 0;

            registers.PC++;

            return InstructionCycles.XorHL;
        }

        /// <summary>
        /// DEC r | r := r - 1
        /// 
        /// Substract 1 from the contents of register 'r' by 1.
        /// 
        /// Affected flags:
        ///     H: '1' if the operation results in carrying or borrowing to bit
        /// 3, '0' otherwise.
        ///     N: '1' regardless of the result.
        ///     Z: '1' if 'r' is set to zero, '0' otherwise.
        /// 
        /// Cycles: 1
        /// </summary>
        public int DecR(byte instruction, IRegisters registers)
        {
#warning Untested method
            int r = instruction >> 3 & 0b111;

            byte oldValue = registers.Read(r);
            byte newValue = (byte)(oldValue - 1);

            registers.Write(r, newValue);

            registers.FlagH = ((oldValue & 0xF) - (newValue & 0xF)) < 0;
            registers.FlagN = true;
            registers.FlagZ = newValue == 0;

            registers.PC++;

            return InstructionCycles.DecR;
        }

        /// <summary>
        /// DEC (HL) | (HL) := (HL) - 1
        /// 
        /// Decrements by 1 the contents of memory specified by register pair HL.
        /// 
        /// Affected flags:
        ///     H: '1' if the operation results in carrying or borrowing to bit
        /// 3, '0' otherwise.
        ///     N: '1' regardless of the result.
        ///     Z: '1' if 'r' is set to zero, '0' otherwise.
        /// 
        /// Cycles: 3
        /// </summary>
        public int DecHL(IRegisters registers, IMemory memory)
        {
#warning Untested method
            ushort address = registers.HL;

            byte oldValue = memory[address];
            byte newValue = (byte)(oldValue - 1);

            memory[address] = newValue;

            registers.FlagH = ((oldValue & 0xF) - (newValue & 0xF)) < 0;
            registers.FlagN = true;
            registers.FlagZ = newValue == 0;

            registers.PC++;

            return InstructionCycles.DecHL;
        }
        #endregion

        #region Jump instructions
        /// <summary>
        /// JP nn | PC := nn
        /// 
        /// Loads the content of the 16-bit address to the program counter (PC).
        /// 'nn' specifies the address of the subsequently executed instruction.
        /// The lower-order byte is placed in byte 2 f the object code and the
        /// higher-order byte is placed in byte 3.
        /// 
        /// Affected flags: None
        /// 
        /// Cycles: 4
        /// </summary>
        public int JpNn(IRegisters registers, IMemory memory)
        {
#warning Untested method
            byte lAddr = memory[(ushort)(++registers.PC)];
            byte hAddr = memory[(ushort)(++registers.PC)];

            registers.PC = (ushort)(hAddr << 8 | lAddr);

            return InstructionCycles.JpNn;
        }

        /// <summary>
        /// JR cc, e | if cc true, PC <- PC + e
        /// 
        /// If condition cc and the flag status match, jumps -127 to +129 steps
        /// from the current address. If cc and the flag status do not match,
        /// the instruction following the current JP instruction is executed.
        /// 
        /// Affected flags: None
        /// 
        /// Cycles 2 (if cc false), 3 (if cc true).
        /// </summary>
        /// <param name="registers"></param>
        /// <param name="memory"></param>
        /// <returns></returns>
        public int JrCcE(byte instruction, IRegisters registers, IMemory memory)
        {
            sbyte e = (sbyte)memory[(ushort)(++registers.PC)];

            int cc = instruction >> 3 & 0b00000011;

            if (!JpCondition(cc, registers))
            {
                registers.PC++;
                return 2;
            }

            short signedE = (short)(e + 1);
            ushort unsignedE = (ushort)Math.Abs(signedE);

            if (signedE > 0)
                registers.PC += unsignedE;
            else
                registers.PC -= unsignedE;

            return 3;
        }

        static bool JpCondition(int condition, IRegisters registers)
        {
            switch (condition)
            {
                case 0b00: // Not Z
                    return !registers.FlagZ;

                case 0b01: // Z
                    return registers.FlagZ;

                case 0b10: // Not C
                    return !registers.FlagCY;

                case 0b11: // C
                    return registers.FlagCY;
            }

            throw new InvalidOperationException(
                $"Invalid jump condition {condition}");
        }
        #endregion

        #region General-Purpose Arithmetic Operations and CPU Control Instructions
        /// <summary>
        /// NOP | No operation
        /// 
        /// Only advances the program counter by 1; performs no other operations
        /// that have an effect.
        /// 
        /// Affected flags: none
        /// 
        /// Cycles: 1
        /// </summary>
        public int Nop(IRegisters registers)
        {
#warning Untested method
#warning Not checking if interruptions are disabled
            registers.PC++;

            return InstructionCycles.Nop;
        }
        #endregion
    }
}
