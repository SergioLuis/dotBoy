namespace DotBoy.Interfaces
{
    public interface IClockDivider
    {
        long MsPerStep { get; }
        long MsLeft { get; }

        void AddObserver(IClockObserver observer);
        void Trigger();
    }
}
