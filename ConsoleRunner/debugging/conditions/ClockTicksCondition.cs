using DotBoy.Interfaces;

namespace ConsoleRunner.Debugging.Conditions
{
    internal class ClockTicksCondition : ICondition
    {
        internal ClockTicksCondition(long targetTicks, IClock clock)
        {
            mTargetTicks = targetTicks;
            mClock = clock;
        }

        bool ICondition.IsConditionMet() => mClock.TimesUpdated == mTargetTicks;

        readonly long mTargetTicks;
        readonly IClock mClock;
    }
}
