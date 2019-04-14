using System;

using NLog;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation.Logging
{
    public class LoggedMemory : IMemory
    {
        int IMemory.Size
        {
            get
            {
                int result = mInternal.Size;
                mLog.Trace("R[SIZE] -> {0}", result);
                return result;
            }
        }

        byte IMemory.this[ushort address]
        {
            get
            {
                byte value = mInternal[address];
                TraceRead(address, value);
                return value;
            }
            set
            {
                TraceWrite(address, value);
                mInternal[address] = value;
            }
        }

        public LoggedMemory(IMemory memory)
        {
            mInternal = memory;
        }

        void TraceRead(ushort address, byte value)
        {
            mLog.Trace(
                "R[0x{0:X4} / {0}] -> 0x{1:X2} / {1} / {2}",
                address,
                value,
                Convert.ToString(value, 2).PadLeft(8, '0'));
        }

        void TraceWrite(ushort address, byte value)
        {
            mLog.Trace(
                "W[0x{0:X4} / {0}] <- 0x{1:X2} / {1} / {2}",
                address,
                value,
                Convert.ToString(value, 2).PadLeft(8, '0'));
        }

        void IMemory.BlockLoad(
            byte[] source,
            uint sourceIndex,
            uint destinationIndex,
            uint length)
        {
            mLog.Trace(
                "BlockLoad: srcIndex: 0x{0:X4} | dstIndex: 0x{1:X4} | length: {2} B",
                sourceIndex,
                destinationIndex,
                length);

            mInternal.BlockLoad(
                source,
                sourceIndex,
                destinationIndex,
                length);
        }

        readonly IMemory mInternal;
        static readonly Logger mLog = LogManager.GetLogger("Memory");
    }
}
