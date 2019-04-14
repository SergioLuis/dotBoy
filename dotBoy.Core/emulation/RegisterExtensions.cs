using System;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation
{
    /*
     * From Nintendo's GameBoy official documentation, chapter 4, section 2
     * (page 95, "codes for registers r and r')
     */
    public static class RegisterOps
    {
        public static void Copy(this IRegisters registers, int srcRegCode, int dstRegCode)
        {
            registers.Write(dstRegCode, registers.Read(srcRegCode));
        }

        public static void Increment(this IRegisters registers, int registerCode, byte value)
        {
            registers.Write(registerCode, (byte)(registers.Read(registerCode) + value));
        }

        public static void Decrement(this IRegisters registers, int registerCode, byte value)
        {
            registers.Write(registerCode, (byte)(registers.Read(registerCode) - value));
        }

        public static byte Read(this IRegisters registers, int registerCode)
        {
            switch (registerCode)
            {
                case A_CODE:
                    return registers.A;

                case B_CODE:
                    return registers.B;

                case C_CODE:
                    return registers.C;

                case D_CODE:
                    return registers.D;

                case E_CODE:
                    return registers.E;

                case H_CODE:
                    return registers.H;

                case L_CODE:
                    return registers.L;
            }

            throw new ArgumentOutOfRangeException(string.Format(
                "Invalid register Id: {0}",
                Convert.ToString(registerCode, 2).PadLeft(3, '0')));
        }

        public static void Write(this IRegisters registers, int registerCode, byte value)
        {
            switch (registerCode)
            {
                case A_CODE:
                    registers.A = (byte)value;
                    return;

                case B_CODE:
                    registers.B = (byte)value;
                    return;

                case C_CODE:
                    registers.C = (byte)value;
                    return;

                case D_CODE:
                    registers.D = (byte)value;
                    return;

                case E_CODE:
                    registers.E = (byte)value;
                    return;

                case H_CODE:
                    registers.H = (byte)value;
                    return;

                case L_CODE:
                    registers.L = (byte)value;
                    return;
            }

            throw new ArgumentOutOfRangeException(string.Format(
                "Invalid register Id: {0}",
                Convert.ToString(registerCode, 2).PadLeft(3, '0')));
        }

        const int A_CODE = 0b111;
        const int B_CODE = 0b000;
        const int C_CODE = 0b001;
        const int D_CODE = 0b010;
        const int E_CODE = 0b011;
        const int H_CODE = 0b100;
        const int L_CODE = 0b101;
    }
}
