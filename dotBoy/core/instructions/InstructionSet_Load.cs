using System;

using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core.Instructions
{
    public partial class InstructionSet
    {
#warning Untested instruction
        public void LdRR(byte instruction, IRegisters registers)
        {
            byte destination = (byte)(instruction & 0x38);
            byte source = (byte)(instruction & 0x07);

            throw new NotImplementedException();
        }
    }
}
