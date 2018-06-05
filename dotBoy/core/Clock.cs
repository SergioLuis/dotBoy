using System;

using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Clock : IClock
    {
        long IClock.Ms => mMs;

        IClock IClock.Update()
        {
            // https://msdn.microsoft.com/en-us/library/system.datetime.ticks(v=vs.110).aspx
            mMs = DateTime.UtcNow.Ticks / 10_000;
            return this;
        }

        long mMs;
    }
}
