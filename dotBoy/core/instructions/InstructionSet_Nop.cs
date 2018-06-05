using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core.Instructions
{
    public partial class InstructionSet
    {
#warning Untested instruction
        public void Nop(IRegisters registers)
        {
#warning Not checking if interruptions are disabled
            registers.PC++;
        }
    }
}
