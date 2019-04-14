using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation
{
    public class DefaultSleeper : ISleeper
    {
        void ISleeper.Sleep(long ms)
        {
            System.Threading.Thread.Sleep((int)ms);
        }
    }
}
