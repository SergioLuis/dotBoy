using NLog;

using SharpBoy.Interfaces;

namespace SharpBoy.Core.Logging
{
    public class LoggedMemory : IMemory
    {
        int IMemory.Size
        {
            get
            {
                int result = mInternal.Size;
                mLog.Trace("Read SIZE -> {0}", result);
                return result;
            }
        }

        byte IMemory.this[ushort address]
        {
            get
            {
                byte result = mInternal[address];
                mLog.Trace("Read [0x{0:X4}] -> 0x{1:X2}", address, result);
                return result;
            }
            set
            {
                mLog.Trace("Write [0x{0:X4}] <- 0x{1:X2}", address, value);
                mInternal[address] = value;
            }
        }

        public LoggedMemory(IMemory memory)
        {
            mInternal = memory;
        }

        void IMemory.BlockLoad(
            byte[] source,
            uint sourceIndex,
            uint destinationIndex,
            uint length)
        {
            mLog.Trace(
                "BlockLoad: srcIndex: 0x{0:X4} | dstIndex: 0x{1:X4} | length: {2}B",
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
