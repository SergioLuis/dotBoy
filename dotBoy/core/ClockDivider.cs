using System;
using System.Collections.Generic;
using System.Text;

using DotBoy.Interfaces;

namespace DotBoy.Core
{
#warning Untested class
    public class ClockDivider : IClockDivider
    {
        public static ClockDivider FromHertz(IClock clock, long hertz)
        {
            return new ClockDivider(clock, 1000 / hertz);
        }

        public static ClockDivider FromMillisPerStep(IClock clock, long millisPerStep)
        {
            return new ClockDivider(clock, millisPerStep);
        }

        protected ClockDivider(IClock clock, long millisPerStep)
        {
            if (millisPerStep < 0)
                throw new ArgumentOutOfRangeException(
                    string.Format("millisPerStep should be positive, but it is {0}", millisPerStep));

            mClock = clock;
            mMillisPerStep = millisPerStep;
            mObservers = new List<IClockObserver>();

            mThis = this;
        }

        long IClockDivider.MsPerStep => mMillisPerStep;

        long IClockDivider.MsLeft => (mLastTime + mMillisPerStep) - mClock.Ms;

        void IClockDivider.AddObserver(IClockObserver observer)
        {
            mObservers.Add(observer);
        }

        void IClockDivider.Trigger()
        {
            if (mThis.MsLeft > 0)
                return;

            foreach (var observer in mObservers)
                observer.OnClockTick();

            mLastTime += mMillisPerStep;
        }

        long mLastTime;

        readonly IClock mClock;
        readonly long mMillisPerStep;
        readonly IClockDivider mThis;

        readonly List<IClockObserver> mObservers;
    }
}
