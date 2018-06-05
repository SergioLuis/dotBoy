using System;

using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Clock : IClock
    {
        long IClock.Millis => mMillis;

        IClock IClock.Update()
        {
            mMillis = DateTime.UtcNow.Ticks;
            return this;
        }

        long mMillis;
    }
}
