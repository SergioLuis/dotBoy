using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoy.Interfaces
{
    public interface ICpu
    {
        void StartSyncrhonousExecution();
        void StartAsynchronousExecution();
    }
}
