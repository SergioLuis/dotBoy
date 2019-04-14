using System;

using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation
{
#warning Untested class
    public class Chronometer : IChronometer
    {
        public Chronometer(IClock clock)
        {
            mClock = clock;
        }

        bool IChronometer.IsRunning => mbIsRunning;

        long IClock.Ms => mClock.Ms - mIdleTime;

        long IClock.TimesUpdated => mTimesUpdated;

        void IChronometer.Start()
        {
            if (mbIsRunning)
                return;

            mClock.Update();
            mIdleTime += mClock.Ms - mLastStop;
            mbIsRunning = true;
        }

        void IChronometer.Stop()
        {
            if (!mbIsRunning)
                return;

            mbIsRunning = false;
            mLastStop = mClock.Ms;
        }

        IClock IClock.Update()
        {
            if (!mbIsRunning)
                throw new InvalidOperationException("The chronometer was not started!");

            mClockMs = mClock.Update().Ms;
            mTimesUpdated++;
            return this;
        }

        bool mbIsRunning = false;
        long mIdleTime = 0;
        long mLastStop = 0;
        long mClockMs = 0;

        long mTimesUpdated;

        readonly IClock mClock;
    }
}
