using System;

using NLog;

using DotBoy.Core.Instructions;
using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Pipeline : IPipeline
    {
        void IPipeline.DecodeAndExecute(
            byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Info(
                "Decoding and executing: {0:D8}",
                Convert.ToString(instruction, 2));

            switch (instruction)
            {
                case 0x00:
                    NopInstr.ExecuteNop(registers);
                    return;

                //case 0xAF:
                //    LoadInstr.Execute_Ld_r_r(instruction, registers);
                //    return;

                case 0xC3:
                    JumpInstr.Execute_Jp_nn(registers, memory);
                    return;

                default:
                    mLog.Error("Instruction not implemented.");
                    throw new NotImplementedException();
            }
        }

        static readonly Logger mLog = LogManager.GetLogger("Pipeline");
    }
}
