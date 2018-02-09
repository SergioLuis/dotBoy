using System.Text;

namespace SharpBoy.Utils
{
    public class ByteArray
    {
        public static bool Equals(
            byte[] left,
            int leftOffset,
            byte[] right,
            int rightOffset,
            int count)
        {
            if (leftOffset + count > left.Length)
                return false;

            if (rightOffset + count > right.Length)
                return false;

            for (int i = 0; i < count; i++)
            {
                if (left[leftOffset + i] != right[rightOffset + i])
                    return false;
            }

            return true;
        }

        public static string GetAsciiString(
            byte[] bytes, int offset, int count)
        {
            int validChars = 0;
            for (int i = offset; i < offset + count; i++)
            {
                if (bytes[i] == 0x00)
                    break;

                validChars++;
            }

            return Encoding.ASCII.GetString(bytes, offset, validChars);
        }
    }
}
