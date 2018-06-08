using System;
using System.Linq;

namespace ConsoleRunner.Debugging.Commands
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
