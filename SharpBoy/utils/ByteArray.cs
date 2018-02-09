using System.Text;

namespace SharpBoy.Utils
{
    public class ByteArray
    {
#warning Untested method
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

#warning Untested method
        public static string GetAsciiString(
            byte[] bytes, int offset, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = offset + count - 1; i >= offset; i--)
            {
                if (bytes[i] != 0x00)
                    break;

                count--;
            }

            return Encoding.ASCII.GetString(bytes, offset, count);
        }
    }
}
