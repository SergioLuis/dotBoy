using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoy.Interfaces
{
    public interface IMemory
    {
        byte this[int address] { get; set; }
        int Size { get; }
    }
}
