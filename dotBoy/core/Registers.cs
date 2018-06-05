using NLog;
using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Registers : IRegisters
    {
        byte IRegisters.A { get; set; }
        byte IRegisters.F { get; set; }

        byte IRegisters.B { get => mBC[0]; set => mBC[0] = value; }
        byte IRegisters.C { get => mBC[1]; set => mBC[1] = value; }

        byte[] IRegisters.BC => mBC;

        byte IRegisters.D { get => mDE[0]; set => mDE[0] = value; }
        byte IRegisters.E { get => mDE[1]; set => mDE[1] = value; }

        byte[] IRegisters.DE => mDE;

        byte IRegisters.H { get => mHL[0]; set => mHL[0] = value; }
        byte IRegisters.L { get => mHL[1]; set => mHL[1] = value; }

        byte[] IRegisters.HL => mHL;

        ushort IRegisters.SP { get; set; }
        ushort IRegisters.PC { get { return mPC; } set { mPC = value; } }

        byte[] mBC = new byte[2];
        byte[] mDE = new byte[2];
        byte[] mHL = new byte[2];

        ushort mPC;

        static readonly Logger mLog = LogManager.GetLogger("Registers");
    }
}
