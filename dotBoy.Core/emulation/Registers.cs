using DotBoy.Core.Interfaces;

namespace DotBoy.Core.Emulation
{
    public class Registers : IRegisters
    {
        public byte A { get; set; }
        public byte F { get; set; }

        public bool FlagZ
        {
            get => (F >> 7 & 0b1) == 1;
            set
            {
                if (value)
                    F |= 0b10000000;
                else
                    F &= 0b01111111;
            }
        }

        public bool FlagN
        {
            get => (F >> 6 & 0b1) == 1;
            set
            {
                if (value)
                    F |= 0b01000000;
                else
                    F &= 0b10111111;
            }
        }

        public bool FlagH
        {
            get => (F >> 5 & 0b1) == 1;
            set
            {
                if (value)
                    F |= 0b00100000;
                else
                    F &= 0b11011111;
            }
        }

        public bool FlagCY
        {
            get => (F >> 4 & 0b1) == 1;
            set
            {
                if (value)
                    F |= 0b00010000;
                else
                    F &= 0b11101111;
            }
        }

        public byte B { get; set; }
        public byte C { get; set; }

        public ushort BC => (ushort)(B << 8 | C);

        public byte D { get; set; }
        public byte E { get; set; }

        public ushort DE => (ushort)(D << 8 | E);

        public byte H { get; set; }
        public byte L { get; set; }

        public ushort HL
        {
            get => (ushort)(H << 8 | L);
            set
            {
                H = (byte)(value >> 8 & 0xF);
                L = (byte)(value & 0xF);
            }
        }

        public ushort SP { get; set; }
        public ushort PC { get; set; }
    }
}
