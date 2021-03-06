﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotBoy.Interfaces
{
    public interface IRegisters
    {
        byte A { get; set; }
        byte F { get; set; }

        byte B { get; set; }
        byte C { get; set; }
        byte[] BC { get; }

        byte D { get; set; }
        byte E { get; set; }
        byte[] DE { get; }

        byte H { get; set; }
        byte L { get; set; }
        byte[] HL { get; }

        ushort SP { get; set; }
        ushort PC { get; set; }
    }
}
