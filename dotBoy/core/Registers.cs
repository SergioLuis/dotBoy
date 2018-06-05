using NLog;
using DotBoy.Interfaces;

namespace DotBoy.Core
{
    public class Registers : IRegisters
    {
        public byte A { get; set; }
        public byte F { get; set; }

        public byte B { get; set; }
        public byte C { get; set; }

        public ushort BC => (ushort)(B << 8 | C);

        public byte D { get; set; }
        public byte E { get; set; }

        public ushort DE => (ushort)(D << 8 | E);

        public byte H { get; set; }
        public byte L { get; set; }

        public ushort HL => (ushort)(H << 8 | L);

        public ushort SP { get; set; }
        public ushort PC { get; set; }
    }
}
