using SharpBoy.Interfaces;

namespace SharpBoy.Core
{
    public class Clock : IClock
    {
        void IClock.WaitUntilNextCycle()
        {
            return;
        }
    }
}
