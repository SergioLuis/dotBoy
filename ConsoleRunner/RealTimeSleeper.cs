using System;
using System.Collections.Generic;
using System.Text;

using DotBoy.Interfaces;

namespace ConsoleRunner
{
    class RealTimeSleeper : ISleeper
    {
        void ISleeper.Sleep(long millis)
        {
            return;
        }
    }
}
