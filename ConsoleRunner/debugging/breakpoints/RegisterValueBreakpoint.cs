using System;
using System.Collections.Generic;

using DotBoy.Interfaces;

namespace ConsoleRunner.Debugging.Breakpoints
{
    internal class RegisterValueBreakpoint : BaseBreakpoint
    {
        internal string RegisterName { get; }
        internal ushort RegisterValue { get; }

        internal static bool TryCreate(
            string registerName,
            ushort registerValue,
            out BaseBreakpoint breakpoint)
        {
            var validRegisters = new HashSet<string>(
                Constants.Registers.Names,
                StringComparer.InvariantCultureIgnoreCase);

            if (!validRegisters.Contains(registerName))
            {
                breakpoint = null;
                return false;
            }

            breakpoint = new RegisterValueBreakpoint(registerName, registerValue);
            return true;
        }

        RegisterValueBreakpoint(string registerName, ushort registerValue)
            : base(
                  condition: Localization.GetString(
                      Localization.Names.RegisterValueBreakpointCondition,
                      registerName,
                      registerValue),
                  description: Localization.GetString(
                      Localization.Names.RegisterValueBreakpointDescription,
                      registerName,
                      registerValue))
        {
            RegisterName = registerName;
            RegisterValue = registerValue;
        }

        internal override bool ShouldTrigger(DebuggingItems debuggingitems)
        {
            IRegisters registers = debuggingitems.Registers;

            switch (RegisterName.ToLowerInvariant())
            {
                case Constants.Registers.A:
                    return registers.A == RegisterValue;

                case Constants.Registers.F:
                    return registers.F == RegisterValue;

                case Constants.Registers.B:
                    return registers.B == RegisterValue;

                case Constants.Registers.C:
                    return registers.C == RegisterValue;

                case Constants.Registers.D:
                    return registers.D == RegisterValue;

                case Constants.Registers.E:
                    return registers.E == RegisterValue;

                case Constants.Registers.H:
                    return registers.H == RegisterValue;

                case Constants.Registers.L:
                    return registers.L == RegisterValue;

                case Constants.Registers.SP:
                    return registers.SP == RegisterValue;

                case Constants.Registers.PC:
                    return registers.PC == RegisterValue;
            }

            return false;
        }
    }
}
