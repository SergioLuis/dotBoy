using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoy.Interfaces
{
    public interface IMemory
    {
        byte this[short address] { get; set; }
        int Size { get; }
    }
}
