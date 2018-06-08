namespace ConsoleRunner.Debugging
{
    internal static class Constants
    {
        internal static class Registers
        {
            internal const string A = "a";
            internal const string F = "f";
            internal const string B = "b";
            internal const string C = "c";
            internal const string D = "d";
            internal const string E = "e";
            internal const string H = "h";
            internal const string L = "l";
            internal const string SP = "sp";
            internal const string PC = "pc";

            internal static readonly string[] Names = new string[]
            {
                A, F, B, C, D, E, H, L, SP, PC
            };
        }
    }
}
