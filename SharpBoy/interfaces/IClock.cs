using System;
using System.Collections.Generic;
using System.Text;

namespace DotBoy.Interfaces
{
    public interface IClock
    {
        void WaitUntilNextCycle();
    }
}
