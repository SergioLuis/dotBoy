namespace DotBoy.Interfaces
{
    public interface IInstructionSet
    {
        void JpNn(IRegisters registers, IMemory memory);
        void LdRR(byte instruction, IRegisters registers);
        void Nop(IRegisters registers);
    }
}
