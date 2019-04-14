namespace DotBoy.Core.Interfaces
{
    public interface IClockDivider
    {
        long MsPerStep { get; set; }
        long MsLeft { get; }

        void AddObserver(IClockObserver observer);
        bool Trigger();
    }
}
