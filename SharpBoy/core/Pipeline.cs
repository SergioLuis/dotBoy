using System;
using System.Collections.Generic;
using System.Text;

using SharpBoy.Interfaces;

namespace SharpBoy.Core
{
    public class Pipeline : IPipeline
    {
        void IPipeline.DecodeAndExecute(
            byte instruction, IRegisters registers, IMemory memory)
        {
            throw new NotImplementedException();
        }
    }
}
