using System.Collections.Generic;

using ConsoleRunner.Debugging.Breakpoints;

namespace ConsoleRunner.Debugging.Conditions
{
    internal class BreakpointCondition : ICondition
    {
        internal BaseBreakpoint TriggeredBreakpoint => mTriggeredBreakpoint;

        internal BreakpointCondition(IList<BaseBreakpoint> breakpoints)
        {
            mBreakpoints = breakpoints;
        }

        bool ICondition.IsConditionMet()
        {
            foreach (var breakpoint in mBreakpoints)
            {
                if (!breakpoint.ShouldTrigger())
                    continue;

                mTriggeredBreakpoint = breakpoint;
                return true;
            }

            return false;
        }

        BaseBreakpoint mTriggeredBreakpoint;
        readonly IList<BaseBreakpoint> mBreakpoints;
    }
}
