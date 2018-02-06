using System;
using System.Collections.Generic;
using System.Text;

using SharpBoy.Interfaces;

namespace SharpBoy.Core
{
    public class Clock : IClock
    {
        void IClock.WaitUntilNextCycle()
        {
            throw new NotImplementedException();
        }
    }
}
