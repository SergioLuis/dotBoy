using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoy.Interfaces
{
    public interface IClock
    {
        void WaitUntilNextCycle();
    }
}
