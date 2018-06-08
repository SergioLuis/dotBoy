namespace ConsoleRunner.Debugging.Breakpoints
{
    internal abstract class BaseBreakpoint
    {
        internal string Condition { get; }
        internal string Description { get; }
        internal bool Enabled { get; set; }

        internal BaseBreakpoint(string condition, string description)
        {
            Condition = condition;
            Description = description;

            Enabled = true;
        }

        internal abstract bool ShouldTrigger(DebuggingItems items);
    }
}
