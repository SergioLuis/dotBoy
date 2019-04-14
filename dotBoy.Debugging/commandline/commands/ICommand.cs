using DotBoy.Debugging;

namespace DotBoy.Debugging.CommandLine
{
    internal interface ICommand
    {
        string Name { get; }
        string Description { get; }
        string Prompt { get; }

        bool CanExecute(string[] args, DebuggingItems items);
        void Execute(string[] args, DebuggingItems items);
        void ExecuteInteractive(DebuggingItems items);
    }
}
