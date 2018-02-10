using Xunit;

using SharpBoy.Utils;

namespace SharpBoy.Tests
{
    public class ByteArrayTests
    {
        [Fact]
        public void EqualsTest()
        {
            byte[] left = { 183, 14, 158, 69, 123, 94, 115, 220 };
            byte[] right = { 32, 158, 69, 123, 94, 57, 227, 220 };

            Assert.True(
                ByteArray.Equals(left, 0, left, 0, (uint)left.Length));

            Assert.True(
                ByteArray.Equals(left, 2, right, 1, 4));

            Assert.False(
                ByteArray.Equals(left, 2, right, 1, 5));

            Assert.False(
                ByteArray.Equals(left, 7, right, 7, 2));
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
