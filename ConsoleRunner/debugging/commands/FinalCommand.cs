using System;

namespace ConsoleRunner.Debugging.Commands
{
    internal abstract class FinalCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public string Prompt => throw new InvalidOperationException();

        public abstract bool CanExecute(string[] args, DebuggingItems items);
        public abstract void Execute(string[] args, DebuggingItems items);
        public void ExecuteInteractive(DebuggingItems items) => throw new InvalidOperationException();
    }
}
