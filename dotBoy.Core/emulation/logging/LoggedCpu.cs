using NLog;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation.Logging
{
    public class LoggedCpu : IClockObserver
    {
        public LoggedCpu(IClockObserver cpu)
        {
            mCpu = cpu;
        }

        bool IClockObserver.OnClockTick()
        {
            mLog.Info("CPU cycle started.");
            bool succeeded = mCpu.OnClockTick();
            mLog.Info("CPU cycle finished");
            return succeeded;
        }

        readonly IClockObserver mCpu;

        static readonly Logger mLog = LogManager.GetLogger("CPU");
    }
}
