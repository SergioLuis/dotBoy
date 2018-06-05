using System;

using NLog;

using DotBoy.Core.Instructions;
using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Pipeline : IPipeline
    {
        public Pipeline(IInstructionSet instructionSet)
        {
            mInstructionSet = instructionSet;
        }

        void IPipeline.DecodeAndExecute(
            byte instruction, IRegisters registers, IMemory memory)
        {
            switch (instruction)
            {
                case 0x00:
                    mInstructionSet.Nop(registers);
                    return;

                case 0xAF:
                    mInstructionSet.LdRR(instruction, registers);
                    return;

                case 0xC3:
                    mInstructionSet.JpNn(registers, memory);
                    return;

                default:
                    HaltAndCatchFire(instruction);
                    break;
            }
        }

        void HaltAndCatchFire(byte instruction)
        {
            mLog.Error("Instruction not implemented.");
            throw new NotImplementedException();
        }

        readonly IInstructionSet mInstructionSet;

        static readonly Logger mLog = LogManager.GetLogger("Pipeline");
    }
}
