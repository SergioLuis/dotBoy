using System;
using System.Collections.Generic;
using System.Text;

using SharpBoy.Interfaces;

namespace SharpBoy.Core
{
    public class Memory : IMemory
    {
        byte IMemory.this[int address] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IMemory.Size { get => throw new NotImplementedException(); }
    }
}
