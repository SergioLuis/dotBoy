using System;
using System.Collections.Generic;
using System.Text;

namespace DotBoy.Interfaces
{
    public interface ICpu
    {
        void StartSynchronousExecution();
        void StartAsynchronousExecution();
    }
}
