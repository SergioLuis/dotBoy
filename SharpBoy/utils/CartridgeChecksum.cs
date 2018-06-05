using System;

namespace DotBoy.Utils
{
    public static class CartridgeChecksum
    {
        public static int CalculateHeaderChecksum(
            byte[] bytes,
            int index,
            int length)
        {
            int result = 0;
            for (int i = index; i < index + length; i++)
                result -= (bytes[i] + 1);

            return result;
        }

        public static uint CalculateChecksum(
            byte[] bytes,
            int index,
            int length,
            params ushort[] excludedAddresses)
        {
            uint result = 0;
            for (int i = index; i < index + length; i++)
            {
                if (Array.IndexOf(excludedAddresses, i) > -1)
                    continue;

                result += bytes[i];
            }

            return result;
        }
    }
}
