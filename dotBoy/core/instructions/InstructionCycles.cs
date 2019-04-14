namespace DotBoy.Core.Instructions
{
    public static class InstructionCycles
    {
        public const int LdRR  = 1;
        public const int LdRN  = 2;
        public const int LdHLN = 3;
        public const int LdHLiA = 2;
        public const int LdHLdA = 2;
        public const int LdDdNn = 3;
        public const int XorR = 1;
        public const int XorN = 2;
        public const int XorHL = 2;
        public const int DecR = 1;
        public const int DecHL = 3;
        public const int JpNn = 4;
        public const int Nop = 1;
    }
}
