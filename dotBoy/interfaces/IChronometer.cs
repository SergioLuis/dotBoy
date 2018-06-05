namespace DotBoy.Interfaces
{
    public interface IChronometer : IClock
    {
        bool IsRunning { get; }

        void Start();
        void Stop();
    }
}
