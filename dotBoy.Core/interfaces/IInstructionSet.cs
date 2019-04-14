namespace DotBoy.Core.Interfaces
{
    public interface IInstructionSet
    {
        // 8-bit Transfer and Input/Output Instructions
        int LdRR(byte instruction, IRegisters registers);
        int LdRN(byte instruction, IRegisters registers, IMemory memory);
        int LdHLN(IRegisters registers, IMemory memory);
        int LdHLiA(IRegisters registers, IMemory memory);
        int LdHLdA(IRegisters registers, IMemory memory);

        // 16-Bit Transfer Instructions
        int LdDdNn(byte instruction, IRegisters registers, IMemory memory);

        // 8-Bit Arithmetic and Logical Operation Instructions
        int XorR(byte instruction, IRegisters registers);
        int XorN(byte instruction, IRegisters registers, IMemory memory);
        int XorHL(byte instruction, IRegisters registers, IMemory memory);
        int DecR(byte instruction, IRegisters registers);
        int DecHL(IRegisters registers, IMemory memory);

        // Jump instructions
        int JpNn(IRegisters registers, IMemory memory);
        int JrCcE(byte instruction, IRegisters registers, IMemory memory);

        // General-Purpose Arithmetic Operations and CPU Control Instructions
        int Nop(IRegisters registers);
    }
}
