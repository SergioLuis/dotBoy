namespace DotBoy.Interfaces
{
    public interface IClock
    {
        long Ms { get; }

        IClock Update();
    }
}
