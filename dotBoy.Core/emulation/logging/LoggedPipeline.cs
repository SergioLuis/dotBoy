using System;
using System.Collections.Generic;
using System.Text;

using NLog;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation.Logging
{
    public class LoggedPipeline : IPipeline
    {
        public LoggedPipeline(IPipeline pipeline)
        {
            mInternal = pipeline;
        }

        bool IPipeline.DecodeAndExecute(
            byte instruction, IRegisters registers, IMemory memory, out int consumedCycles)
        {
            mLog.Info(
                "Decoding and executing: {0} (0x{1:X2})",
                Convert.ToString(instruction, 2).PadLeft(8, '0'),
                instruction);

            return mInternal.DecodeAndExecute(instruction, registers, memory, out consumedCycles);
        }

        IPipeline mInternal;

        static readonly Logger mLog = LogManager.GetLogger("Pipeline");
    }
}
