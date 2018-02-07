using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoy.Interfaces
{
    public interface IPipeline
    {
        void DecodeAndExecute(
            byte instruction, IRegisters registers, IMemory memory);
    }
}
