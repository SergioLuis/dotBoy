using System;

using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core.Instructions
{
    public partial class InstructionSet
    {
#warning Untested instruction
        public void JpNn(IRegisters registers, IMemory memory)
        {
            ushort currentPc = registers.PC;

            byte l = memory[(ushort)(currentPc + 1)];
            byte h = memory[(ushort)(currentPc + 2)];

            registers.PC = (ushort)(h << 8 | l);
        }
    }
}
