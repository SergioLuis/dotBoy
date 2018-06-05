using System;

using NLog;

using SharpBoy.Interfaces;

namespace SharpBoy.Core.Logging
{
    public class LoggedRegisters : IRegisters
    {
        byte IRegisters.A
        {
            get
            {
                byte result = mInternal.A;
                mLog.Trace("Read A -> 0x{0:X2}", result);
                return result;
            }
            set
            {
                mLog.Trace("Write A <- 0x{0:X2}", value);
                mInternal.A = value;
            }
        }

        byte IRegisters.F
        {
            get { return mInternal.F; }
            set { mInternal.F = value; }
        }

        byte IRegisters.B
        {
            get { return mInternal.B; }
            set { mInternal.B = value; }
        }

        byte IRegisters.C
        {
            get { return mInternal.C; }
            set { mInternal.C = value; }
        }

        byte[] IRegisters.BC => throw new NotImplementedException();

        byte IRegisters.D
        {
            get { return mInternal.D; }
            set { mInternal.D = value; }
        }

        byte IRegisters.E
        {
            get { return mInternal.E; }
            set { mInternal.E = value; }
        }

        byte[] IRegisters.DE => throw new NotImplementedException();

        byte IRegisters.H
        {
            get { return mInternal.H; }
            set { mInternal.H = value; }
        }

        byte IRegisters.L
        {
            get { return mInternal.L; }
            set { mInternal.L = value; }
        }

        byte[] IRegisters.HL => throw new NotImplementedException();

        ushort IRegisters.SP
        {
            get { return mInternal.SP; }
            set { mInternal.SP = value; }
        }

        ushort IRegisters.PC
        {
            get
            {
                ushort result = mInternal.PC;
                mLog.Trace("Read PC -> 0x{0:X4}", result);
                return result;
            }
            set
            {
                mLog.Trace("Writing PC <- 0x{0:X4}", value);
                mInternal.PC = value;
            }
        }

        public LoggedRegisters(IRegisters registers)
        {
            mInternal = registers;
        }

        readonly IRegisters mInternal;
        static readonly Logger mLog = LogManager.GetLogger("Registers");
    }
}
