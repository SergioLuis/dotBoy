using NLog;

using DotBoy.Interfaces;

namespace DotBoy.Core.Logging
{
    public class LoggedCpu : IClockObserver
    {
        public LoggedCpu(IClockObserver cpu)
        {
            mCpu = cpu;
        }

        void IClockObserver.OnClockTick()
        {
            mLog.Info("CPU cycle started.");
            mCpu.OnClockTick();
            mLog.Info("CPU cycle finished");
        }

        readonly IClockObserver mCpu;

        static readonly Logger mLog = LogManager.GetLogger("CPU");
    }
}
