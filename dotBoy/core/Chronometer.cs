using System;

using DotBoy.Interfaces;

namespace DotBoy.Core
{
#warning Untested class
    public class Chronometer : IChronometer
    {
        public Chronometer(IClock clock)
        {
            mClock = clock;
        }

        bool IChronometer.IsRunning => mbIsRunning;

        long IClock.Millis => mClock.Millis - mIdleTime;

        void IChronometer.Start()
        {
            if (mbIsRunning)
                return;

            mClock.Update();
            mIdleTime += mClock.Millis - mLastStop;
            mbIsRunning = true;
        }

        void IChronometer.Stop()
        {
            if (!mbIsRunning)
                return;

            mbIsRunning = true;
            mLastStop = mClock.Millis;
        }

        IClock IClock.Update()
        {
            if (!mbIsRunning)
                throw new InvalidOperationException("The chronometer was not started!");

            mClockMillis = mClock.Update().Millis;
            return this;
        }

        bool mbIsRunning = false;
        long mIdleTime = 0;
        long mLastStop = 0;
        long mClockMillis = 0;

        readonly IClock mClock;
    }
}
