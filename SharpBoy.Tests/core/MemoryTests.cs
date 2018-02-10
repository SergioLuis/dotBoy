using System;

using Xunit;

using SharpBoy.Core;
using SharpBoy.Interfaces;

namespace SharpBoy.Tests.Core
{
    public class MemoryTests
    {
        [Fact]
        public void AllZeroesWhenEmptyTest()
        {
            IMemory memory = new Memory();

            for (int i = 0; i < memory.Size; i++)
                Assert.Equal(0x00, memory[(ushort)i]);
        }

        [Fact]
        public void WriteAndReadTest()
        {
            IMemory memory = new Memory();

            Assert.Equal(0x00, memory[0x0000]);
            Assert.Equal(0x00, memory[0x0100]);
            Assert.Equal(0x00, memory[0xFFFF]);

            memory[0x0000] = 0xAA;
            memory[0x0100] = 0xBB;
            memory[0xFFFF] = 0xCC;

            Assert.Equal(0xAA, memory[0x0000]);
            Assert.Equal(0xBB, memory[0x0100]);
            Assert.Equal(0xCC, memory[0xFFFF]);
        }

        [Fact]
        public void WriteToAndReadFromEchoRamTest()
        {
            // ----------------------------- FE00
            //    Echo of 8KB internal RAM
            // ----------------------------- E000
            //
            //         8KB internal RAM      DE00 --
            //                                      | Echoed region
            // ----------------------------- C000 --

            IMemory memory = new Memory();

            Assert.Equal(0x00, memory[0xC000]);
            Assert.Equal(0x00, memory[0xE000]);

            Assert.Equal(0x00, memory[0xE000 - 1]);
            Assert.Equal(0x00, memory[0xFE00 - 1]);

            memory[0xC000] = 0xAA;
            memory[0xDE00 - 1] = 0xBB;

            Assert.Equal(0xAA, memory[0xC000]);
            Assert.Equal(0xAA, memory[0xE000]);

            Assert.Equal(0xBB, memory[0xDE00 - 1]);
            Assert.Equal(0xBB, memory[0xFE00 - 1]);

            memory[0xFE00 - 1] = 0xAA;
            memory[0xE000] = 0xBB;

            Assert.Equal(0xBB, memory[0xC000]);
            Assert.Equal(0xBB, memory[0xE000]);

            Assert.Equal(0xAA, memory[0xDE00 - 1]);
            Assert.Equal(0xAA, memory[0xFE00 - 1]);
        }

        [Fact]
        public void BlockLoadAndReadTest()
        {
            byte[] dummyCartridge = new byte[32 * 1024];

            Random random = new Random();
            random.NextBytes(dummyCartridge);

            IMemory memory = new Memory();
            memory.BlockLoad(
                source: dummyCartridge,
                sourceIndex: 0,
                destinationIndex: 0x00,
                length: (uint)dummyCartridge.Length);

            for (ushort i = 0; i < dummyCartridge.Length; i++)
                Assert.Equal(dummyCartridge[i], memory[i]);
        }

        [Fact]
        public void BlockLoadOutsideSwitchableRegionTest()
        {
            byte[] dummyCartridge = new byte[32 * 1024 + 1];

            IMemory memory = new Memory();
            Assert.ThrowsAny<Exception>(() => memory.BlockLoad(
                source: dummyCartridge,
                sourceIndex: 0,
                destinationIndex: 0x00,
                length: (uint)dummyCartridge.Length));
        }
    }
}
