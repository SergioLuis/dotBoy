using System;

using NLog;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation.Logging
{
    public class LoggedRegisters : IRegisters
    {
        public byte A
        {
            get
            {
                byte value = mInternal.A;
                TraceRegisterRead("A", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("A", value);
                mInternal.A = value;
            }
        }

        public byte F
        {
            get
            {
                byte value = mInternal.F;
                TraceRegisterRead("F", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("F", value);
                mInternal.F = value;
            }
        }

        public bool FlagZ
        {
            get
            {
                bool value = mInternal.FlagZ;
                TraceFlagRead("z", value);
                return value;
            }
            set
            {
                TraceFlagWrite("z", value);
                mInternal.FlagZ = value;
            }
        }

        public bool FlagN
        {
            get
            {
                bool value = mInternal.FlagN;
                TraceFlagRead("n", value);
                return value;
            }
            set
            {
                TraceFlagWrite("n", value);
                mInternal.FlagN = value;
            }
        }

        public bool FlagH
        {
            get
            {
                bool value = mInternal.FlagH;
                TraceFlagRead("h", value);
                return value;
            }
            set
            {
                TraceFlagWrite("h", value);
                mInternal.FlagH = value;
            }
        }

        public bool FlagCY
        {
            get
            {
                bool value = mInternal.FlagCY;
                TraceFlagRead("cy", value);
                return value;
            }
            set
            {
                TraceFlagWrite("cy", value);
                mInternal.FlagCY = value;
            }
        }

        public byte B
        {
            get
            {
                byte value = mInternal.B;
                TraceRegisterRead("B", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("B", value);
                mInternal.B = value;
            }
        }

        public byte C
        {
            get
            {
                byte value = mInternal.C;
                TraceRegisterRead("C", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("C", value);
                mInternal.C = value;
            }
        }

        public ushort BC
        {
            get
            {
                ushort value = mInternal.BC;
                TraceRegisterRead("BC", value);
                return value;
            }
        }

        public byte D
        {
            get
            {
                byte value = mInternal.D;
                TraceRegisterRead("D", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("D", value);
                mInternal.D = value;
            }
        }

        public byte E
        {
            get
            {
                byte value = mInternal.E;
                TraceRegisterRead("E", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("E", value);
                mInternal.E = value;
            }
        }

        public ushort DE
        {
            get
            {
                ushort value = mInternal.DE;
                TraceRegisterRead("DE", value);
                return value;
            }
        }

        public byte H
        {
            get
            {
                byte value = mInternal.H;
                TraceRegisterRead("H", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("H", value);
                mInternal.H = value;
            }
        }

        public byte L
        {
            get
            {
                byte value = mInternal.L;
                TraceRegisterWrite("L", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("L", value);
                mInternal.L = value;
            }
        }

        public ushort HL
        {
            get
            {
                ushort value = mInternal.HL;
                TraceRegisterRead("HL", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("HL", value);
                mInternal.HL = value;
            }
        }

        public ushort SP
        {
            get
            {
                ushort value = mInternal.SP;
                TraceRegisterRead("SP", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("SP", value);
                mInternal.SP = value;
            }
        }

        public ushort PC
        {
            get
            {
                ushort value = mInternal.PC;
                TraceRegisterRead("PC", value);
                return value;
            }
            set
            {
                TraceRegisterWrite("PC", value);
                mInternal.PC = value;
            }
        }

        public LoggedRegisters(IRegisters registers)
        {
            mInternal = registers;
        }

        void TraceRegisterRead(string register, byte value)
        {
            mLog.Trace(
                "R[{0}] -> 0x{1:X2} / {1} / {2}",
                register,
                value,
                Convert.ToString(value, 2).PadLeft(8, '0'));
        }

        void TraceRegisterRead(string register, ushort value)
        {
            mLog.Trace(
                "R[{0}] -> 0x{1:X4} / {1} / {2}",
                register,
                value,
                Convert.ToString(value, 2).PadLeft(16, '0'));
        }

        void TraceFlagRead(string flag, bool value)
        {
            mLog.Trace(
                "R[F({0})] -> {1}", flag, value ? '1' : '0');
        }

        void TraceRegisterWrite(string register, byte value)
        {
            mLog.Trace(
                "W[{0}] <- 0x{1:X2} / {1} / {2}",
                register,
                value,
                Convert.ToString(value, 2).PadLeft(8, '0'));
        }

        void TraceRegisterWrite(string register, ushort value)
        {
            mLog.Trace(
                "W[{0}] <- 0x{1:X4} / {1} / {2}",
                register,
                value,
                Convert.ToString(value, 2).PadLeft(16, '0'));
        }

        void TraceFlagWrite(string flag, bool value)
        {
            mLog.Trace(
                "W[F({0})] <- {1}", flag, value ? '1' : '0');
        }

        readonly IRegisters mInternal;
        static readonly Logger mLog = LogManager.GetLogger("Registers");
    }
}
