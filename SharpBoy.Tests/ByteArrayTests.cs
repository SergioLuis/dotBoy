using System;
using Xunit;

using SharpBoy.Utils;

namespace SharpBoy.Tests
{
    public class ByteArrayTests
    {
        [Fact]
        public void EqualsTest()
        {
            // Same byte array

            // Byte array with slice equals

            // Byte array with slice not equals

            // Byte array with slice does not match
        }

        [Fact]
        public void GetAsciiStringTest()
        {
            byte[] title =
            {
                0xFF,
                0xFF,
                0xFF,
                0x44, // D
                0x4B, // K
                0x20, // _space_
                0x43, // C
                0x4F, // O
                0x55, // U
                0x4E, // N
                0x54, // T
                0x52, // R
                0x59, // Y
                0x00, // \0
                0x42, // B
                0x44, // D
                0x44, // D
                0x45, // E
                0xFF,
                0xFF,
                0xFF
            };

            Assert.Equal(
                "DK COUNTRY",
                ByteArray.GetAsciiString(title, 3, 16));
        }
    }
}
