using System;
using System.Collections.Generic;
using System.Text;

using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core.Logging
{
    public class LoggedPipeline : IPipeline
    {
        public LoggedPipeline(IPipeline pipeline)
        {
            mInternal = pipeline;
        }

        void IPipeline.DecodeAndExecute(byte instruction, IRegisters registers, IMemory memory)
        {
            mLog.Info(
                "Decoding and executing: {0} (0x{1:X2})",
                Convert.ToString(instruction, 2).PadLeft(8, '0'),
                instruction);

            mInternal.DecodeAndExecute(instruction, registers, memory);
        }

        IPipeline mInternal;

        static readonly Logger mLog = LogManager.GetLogger("Pipeline");
    }
}
