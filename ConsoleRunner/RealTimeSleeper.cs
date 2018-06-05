using DotBoy.Interfaces;

namespace ConsoleRunner
{
    class RealTimeSleeper : ISleeper
    {
        void ISleeper.Sleep(long ms)
        {
            System.Threading.Thread.Sleep((int)ms);
        }
    }
}
