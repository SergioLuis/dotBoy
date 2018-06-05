namespace DotBoy.Interfaces
{
    public interface IPipeline
    {
        void DecodeAndExecute(
            byte instruction, IRegisters registers, IMemory memory);
    }
}
