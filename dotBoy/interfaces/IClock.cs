namespace DotBoy.Interfaces
{
    public interface IClock
    {
        long Millis { get; }

        IClock Update();
    }
}
