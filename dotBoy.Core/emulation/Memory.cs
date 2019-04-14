using System;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation
{
    public class Memory : IMemory
    {
        byte IMemory.this[ushort address]
        {
            get => mMemoryBank[address];
            set => Write(mMemoryBank, address, value);
        }

        int IMemory.Size { get => SIZE; }

        void IMemory.BlockLoad(
            byte[] source,
            uint sourceIndex,
            uint destinationIndex,
            uint length)
        {
            if (destinationIndex + length > 0x8000)
            {
#warning Generic exception
#warning Unlocalized string
                throw new Exception(
                    "Cannot directly load data outside of the switchable region.");
            }

            Array.Copy(
                source,
                sourceIndex,
                mMemoryBank,
                destinationIndex,
                length);
        }

        static void Write(byte[] array, ushort address, byte value)
        {
            // ----------------------------- FE00
            //    Echo of 8KB internal RAM
            // ----------------------------- E000
            //
            //         8KB internal RAM      DE00 --
            //                                      | Echoed region
            // ----------------------------- C000 --

            array[address] = value;

            if (address >= 0xC000 && address < 0xDE00)
            {
                ushort echoAddress = (ushort)(0xE000 + address - 0xC000);
                array[echoAddress] = value;
                return;
            }

            if (address >= 0xE000 && address < 0xFE00)
            {
                ushort echoAddress = (ushort)(0xC000 + address - 0xE000);
                array[echoAddress] = value;
                return;
            }
        }

        readonly byte[] mMemoryBank = new byte[SIZE];

        static readonly ushort ECHOED_REGION_SIZE = 7680;

        const int SIZE = 65536;
    }
}
