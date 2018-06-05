using System.Text;

namespace DotBoy.Utils
{
    public class ByteArray
    {
        public static bool Equals(
            byte[] left,
            uint leftIndex,
            byte[] right,
            uint rightIndex,
            uint length)
        {
            if (leftIndex + length > left.Length)
                return false;

            if (rightIndex + length > right.Length)
                return false;

            for (int i = 0; i < length; i++)
            {
                if (left[leftIndex + i] != right[rightIndex + i])
                    return false;
            }

            return true;
        }

        public static string GetAsciiString(
            byte[] bytes, int index, int length)
        {
            int validChars = 0;
            for (int i = index; i < index + length; i++)
            {
                if (bytes[i] == 0x00)
                    break;

                validChars++;
            }

            return Encoding.ASCII.GetString(bytes, index, validChars);
        }
    }
}
