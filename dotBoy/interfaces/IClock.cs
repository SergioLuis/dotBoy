namespace DotBoy.Interfaces
{
    public interface IClock
    {
        long Ms { get; }
        long TimesUpdated { get; }

        IClock Update();
    }
}
