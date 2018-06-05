using System;
using System.Collections.Generic;
using System.Text;

namespace DotBoy.Interfaces
{
    public interface IMemory
    {
        byte this[ushort address] { get; set; }
        int Size { get; }

        void BlockLoad(
            byte[] source,
            uint sourceIndex,
            uint destinationIndex,
            uint length);
    }
}
