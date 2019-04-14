namespace DotBoy.Interfaces
{
    public interface IPipeline
    {
        bool DecodeAndExecute(
            byte instruction,
            IRegisters registers,
            IMemory memory,
            out int cyclesToConsume);
    }
}
