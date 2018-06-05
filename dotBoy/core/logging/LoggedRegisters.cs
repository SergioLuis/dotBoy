using System;

using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core.Logging
{
    public class LoggedRegisters : IRegisters
    {
        byte IRegisters.A
        {
            get
            {
                byte value = mInternal.A;
                TraceRead("A", value);
                return value;
            }
            set
            {
                TraceWrite("A", value);
                mInternal.A = value;
            }
        }

        byte IRegisters.F
        {
            get
            {
                byte value = mInternal.F;
                TraceRead("F", value);
                return value;
            }
            set
            {
                TraceWrite("F", value);
                mInternal.F = value;
            }
        }

        byte IRegisters.B
        {
            get
            {
                byte value = mInternal.B;
                TraceRead("B", value);
                return value;
            }
            set
            {
                TraceWrite("B", value);
                mInternal.B = value;
            }
        }

        byte IRegisters.C
        {
            get
            {
                byte value = mInternal.C;
                TraceRead("C", value);
                return value;
            }
            set
            {
                TraceWrite("C", value);
                mInternal.C = value;
            }
        }

        ushort IRegisters.BC
        {
            get
            {
                ushort value = mInternal.BC;
                TraceRead("BC", value);
                return value;
            }
        }

        byte IRegisters.D
        {
            get
            {
                byte value = mInternal.D;
                TraceRead("D", value);
                return value;
            }
            set
            {
                TraceWrite("D", value);
                mInternal.D = value;
            }
        }

        byte IRegisters.E
        {
            get
            {
                byte value = mInternal.E;
                TraceRead("E", value);
                return value;
            }
            set
            {
                TraceWrite("E", value);
                mInternal.E = value;
            }
        }

        ushort IRegisters.DE
        {
            get
            {
                ushort value = mInternal.DE;
                TraceRead("DE", value);
                return value;
            }
        }

        byte IRegisters.H
        {
            get
            {
                byte value = mInternal.H;
                TraceRead("H", value);
                return value;
            }
            set
            {
                TraceWrite("H", value);
                mInternal.H = value;
            }
        }

        byte IRegisters.L
        {
            get
            {
                byte value = mInternal.L;
                TraceWrite("L", value);
                return value;
            }
            set
            {
                TraceWrite("L", value);
                mInternal.L = value;
            }
        }

        ushort IRegisters.HL
        {
            get
            {
                ushort value = mInternal.HL;
                TraceRead("HL", value);
                return value;
            }
        }

        ushort IRegisters.SP
        {
            get
            {
                ushort value = mInternal.SP;
                TraceRead("SP", value);
                return value;
            }
            set
            {
                TraceWrite("SP", value);
                mInternal.SP = value;
            }
        }

        ushort IRegisters.PC
        {
            get
            {
                ushort value = mInternal.PC;
                TraceRead("PC", value);
                return value;
            }
            set
            {
                TraceWrite("PC", value);
                mInternal.PC = value;
            }
        }

        public LoggedRegisters(IRegisters registers)
        {
            mInternal = registers;
        }

        void TraceRead(string register, byte value)
        {
            mLog.Trace(
                "R[{0}] -> 0x{1:X2} / {1} / {2}",
                register,
                value,
                Convert.ToString(value, 2).PadLeft(8, '0'));
        }

        void TraceRead(string register, ushort value)
        {
            mLog.Trace(
                "R[{0}] -> 0x{1:X4} / {1} / {2}",
                register,
                value,
                Convert.ToString(value, 2).PadLeft(16, '0'));
        }

        void TraceWrite(string register, byte value)
        {
            mLog.Trace(
                "W[{0}] <- 0x{1:X2} / {1} / {2}",
                register,
                value,
                Convert.ToString(value, 2).PadLeft(8, '0'));
        }

        void TraceWrite(string register, ushort value)
        {
            mLog.Trace(
                "W[{0}] <- 0x{1:X4} / {1} / {2}",
                register,
                value,
                Convert.ToString(value, 2).PadLeft(16, '0'));
        }

        readonly IRegisters mInternal;
        static readonly Logger mLog = LogManager.GetLogger("Registers");
    }
}
