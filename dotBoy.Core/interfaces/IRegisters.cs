namespace DotBoy.Core.Interfaces
{
    public interface IRegisters
    {
        byte A { get; set; }
        byte F { get; set; }

        bool FlagZ { get; set; }
        bool FlagN { get; set; }
        bool FlagH { get; set; }
        bool FlagCY { get; set; }

        byte B { get; set; }
        byte C { get; set; }
        ushort BC { get; }

        byte D { get; set; }
        byte E { get; set; }
        ushort DE { get; }

        byte H { get; set; }
        byte L { get; set; }
        ushort HL { get; set; }

        ushort SP { get; set; }
        ushort PC { get; set; }
    }
}
