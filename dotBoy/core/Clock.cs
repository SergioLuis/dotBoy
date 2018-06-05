using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Clock : IClock
    {
        void IClock.WaitUntilNextCycle()
        {
            return;
        }
    }
}
