namespace DotBoy.Interfaces
{
    public interface IInstructionSet
    {
        // 8-bit Transfer and Input/Output Instructions
        void LdRR(byte instruction, IRegisters registers);
        void LdRN(byte instruction, IRegisters registers, IMemory memory);
        void LdHLN(IRegisters registers, IMemory memory);
        void LdHLiA(IRegisters registers, IMemory memory);
        void LdHLdA(IRegisters registers, IMemory memory);

        // 16-Bit Transfer Instructions
        void LdDdNn(byte instruction, IRegisters registers, IMemory memory);

        // 8-Bit Arithmetic and Logical Operation Instructions
        void XorR(byte instruction, IRegisters registers);
        void XorN(byte instruction, IRegisters registers, IMemory memory);
        void XorHL(byte instruction, IRegisters registers, IMemory memory);
        void DecR(byte instruction, IRegisters registers);
        void DecHL(IRegisters registers, IMemory memory);

        // Jump instructions
        void JpNn(IRegisters registers, IMemory memory);

        // General-Purpose Arithmetic Operations and CPU Control Instructions
        void Nop(IRegisters registers);
    }
}
