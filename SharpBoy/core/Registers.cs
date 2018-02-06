using System;
using System.Collections.Generic;
using System.Text;

using SharpBoy.Interfaces;

namespace SharpBoy.Core
{
    public class Registers : IRegisters
    {
        byte IRegisters.A { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        byte IRegisters.F { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        byte IRegisters.B { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        byte IRegisters.C { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        byte[] IRegisters.BC => throw new NotImplementedException();

        byte IRegisters.D { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        byte IRegisters.E { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        byte[] IRegisters.DE => throw new NotImplementedException();

        byte IRegisters.H { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        byte IRegisters.L { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        byte IRegisters.HL => throw new NotImplementedException();

        short IRegisters.SP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        short IRegisters.PC { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
