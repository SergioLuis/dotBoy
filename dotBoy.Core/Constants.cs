namespace DotBoy.Core
{
    public static class Constants
    {
        public static class Registers
        {
            public const string A = "a";
            public const string F = "f";
            public const string B = "b";
            public const string C = "c";
            public const string D = "d";
            public const string E = "e";
            public const string H = "h";
            public const string L = "l";
            public const string SP = "sp";
            public const string PC = "pc";

            public  static readonly string[] Names = new string[]
            {
                A, F, B, C, D, E, H, L, SP, PC
            };
        }

        public static class Flags
        {
            public const string Zero = "z";
            public const string Subs = "n";
            public const string Half = "h";
            public const string Full = "cy";

            public static readonly string[] Names = new string[]
            {
                Zero, Subs, Half, Full
            };
        }
    }
}
