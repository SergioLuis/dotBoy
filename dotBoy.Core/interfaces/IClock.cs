namespace DotBoy.Core.Interfaces
{
    public interface IClock
    {
        long Ms { get; }
        long TimesUpdated { get; }

        IClock Update();
    }
}
