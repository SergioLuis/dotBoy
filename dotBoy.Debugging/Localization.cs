using System;
using System.Collections.Generic;

namespace DotBoy.Debugging
{
    public static class Localization
    {
        public enum Language
        {
            English,
            Spanish
        }

        public enum Names
        {
            Exit,

            RegistersCommandName,
            RegistersCommandDescription,
            RegistersCommandPrompt,

            ReadRegisterCommandName,
            ReadRegisterCommandDescription,
            ReadRegisterCommandPrompt,

            ExecuteReadOneRegisterCommandName,
            ExecuteReadOneRegisterCommandDescription,

            ExecuteReadAllRegistersCommandName,
            ExecuteReadAllRegistersCommandDescription,

            ExecuteReadFlagsCommandName,
            ExecuteReadFlagsCommandDescription,

            WriteRegisterCommandName,
            WriteRegisterCommandDescription,
            WriteRegisterCommandPrompt,

            ExecuteWriteOneRegisterCommandName,
            ExecuteWriteOneRegisterCommandDescription,

            ExecuteResetAllRegistersCommandName,
            ExecuteResetAllRegistersCommandDescription,
            ExecuteResetAllRegistersSuccessMessage,

            ExecuteWriteFlagCommandName,
            ExecuteWriteFlagCommandDescription,
            ExecuteWriteFlagCommandSuccessMessage,

            MemoryCommandName,
            MemoryCommandDescription,
            MemoryCommandPrompt,

            ReadMemoryCommandName,
            ReadMemoryCommandDescription,
            ReadMemoryCommandPrompt,

            ExecuteReadMemoryCommandName,
            ExecuteReadMemoryCommandDescription,
            ExecuteReadMemoryCommandInvalidAddressMessage,

            WriteMemoryCommandName,
            WriteMemoryCommandDescription,
            WriteMemoryCommandPrompt,

            ExecuteWriteMemoryCommandName,
            ExecuteWriteMemoryCommandDescription,
            ExecuteWriteMemoryCommandInvalidAddressMessage,
            ExecuteWriteMemoryCommandInvalidDataMessage,

            ClockCommandName,
            ClockCommandDescription,
            ClockCommandPrompt,

            ClockTicksCommandName,
            ClockTicksCommandDescription,
            ClockTicksCommandPrompt,

            ExecuteTicksCommandName,
            ExecuteTicksCommandDescription,

            BreakpointsCommandName,
            BreakpointsCommandDescription,
            BreakpointsCommandPrompt,

            ExecuteListBreakpointsCommandName,
            ExecuteListBreakpointsCommandDescription,
            ExecuteListBreakpointsCommandNoBreakpointsErrorMessage,

            AddBreakpointCommandName,
            AddBreakpointCommandDescription,
            AddBreakpointCommandPrompt,

            AddBreakpointByRegisterValueCommandName,
            AddBreakpointByRegisterValueCommandDescription,
            AddBreakpointByRegisterValueCommandPrompt,

            ExecuteAddBreakpointByRegisterValueCommandName,
            ExecuteAddBreakpointByRegisterValueCommandDescription,
            ExecuteAddBreakpointByRegisterValueCommandSuccessMessage,
            ExecuteAddBreakpointByRegisterValueCommandFailureMessage,

            EnableBreakpointCommandName,
            EnableBreakpointCommandDescription,
            EnableBreakpointCommandPrompt,

            ExecuteEnableBreakpointCommandName,
            ExecuteEnableBreakpointCommandDescription,
            ExecuteEnableBreakpointCommandSuccessMessage,
            ExecuteEnableBreakpointCommandFailureMessage,

            DisableBreakpointCommandName,
            DisableBreakpointCommandDescription,
            DisableBreakpointCommandPrompt,

            ExecuteDisableBreakpointCommandName,
            ExecuteDisableBreakpointCommandDescription,
            ExecuteDisableBreakpointCommandSuccessMessage,
            ExecuteDisableBreakpointCommandFailureMessage,

            RemoveBreakpointCommandName,
            RemoveBreakpointCommandDescription,
            RemoveBreakpointCommandPrompt,

            ExecuteRemoveBreakpointCommandName,
            ExecuteRemoveBreakpointCommandDescription,
            ExecuteRemoveBreakpointCommandSuccessMessage,
            ExecuteRemoveBreakpointCommandFailureMessage,

            ExecuteRunUntilBreakpointCommandName,
            ExecuteRunUntilBreakpointCommandDescription,
            ExecuteRunUntilBreakpointSuccessMessage,
            ExecuteRunUntilBreakpointFailureMessage,

            RegisterValueBreakpointCondition,
            RegisterValueBreakpointDescription,

            InvalidRegisterErrorMessage,
        }

        public static void TryInitialize(Language language)
        {
            try
            {
                Initialize(language);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    $"There was an error initializing localization: {ex.Message}");
                Console.Error.WriteLine(
                    $"StackTrace:{Environment.NewLine}{ex.StackTrace}");
            }
        }

        static void Initialize(Language language)
        {
            switch (language)
            {
                case Language.English:
                    InitializeEnglish();
                    mbInitialized = true;
                    return;

                case Language.Spanish:
                    InitializeSpanish();
                    mbInitialized = true;
                    return;
            }

            throw new Exception($"Unknown language: {language}");
        }

        public static string GetString(Names key)
        {
            if (!mbInitialized)
                throw new Exception("Localization is not initialized!");

            return mStrings[key];
        }

        public static string GetString(Names key, params object[] args)
        {
            if (!mbInitialized)
                throw new Exception("Localization is not initialized!");

            return string.Format(mStrings[key], args);
        }

        static void InitializeEnglish()
        {
            mStrings = new Dictionary<Names, string>();
            mStrings.Add(Names.Exit, "exit");

            #region RegistersCommand
            mStrings.Add(Names.RegistersCommandName, "registers");
            mStrings.Add(Names.RegistersCommandDescription, "Allows operating directly with the CPU registers.");
            mStrings.Add(Names.RegistersCommandPrompt, "registers");
            #endregion

            #region ReadRegistersCommand
            mStrings.Add(Names.ReadRegisterCommandName, "read");
            mStrings.Add(Names.ReadRegisterCommandDescription, "Allows reading a CPU register directly.");
            mStrings.Add(Names.ReadRegisterCommandPrompt, "registers-read");
            #endregion

            #region ExecuteReadOneRegisterCommand
            mStrings.Add(Names.ExecuteReadOneRegisterCommandName, "[register]");
            mStrings.Add(Names.ExecuteReadOneRegisterCommandDescription, "Reads data from the specified register. Valid registers are ({0}).");
            #endregion

            #region ExecuteReadAllRegistersCommand
            mStrings.Add(Names.ExecuteReadAllRegistersCommandName, "all");
            mStrings.Add(Names.ExecuteReadAllRegistersCommandDescription, "Reads data from all of the registers.");
            #endregion

            #region ExecuteReadFlagsCommand
            mStrings.Add(Names.ExecuteReadFlagsCommandName, "flags");
            mStrings.Add(Names.ExecuteReadFlagsCommandDescription, "Reads and interprets the flags from register (f).");
            #endregion

            #region WriteRegisterCommand
            mStrings.Add(Names.WriteRegisterCommandName, "write");
            mStrings.Add(Names.WriteRegisterCommandDescription, "Allows writing a CPU register directly.");
            mStrings.Add(Names.WriteRegisterCommandPrompt, "registers-write");
            #endregion

            #region ExecuteWriteRegisterCommand
            mStrings.Add(Names.ExecuteWriteOneRegisterCommandName, "[register] [0-9]+");
            mStrings.Add(Names.ExecuteWriteOneRegisterCommandDescription, "Writes data to the specified register. Valid registers are ({0}).");
            #endregion

            #region ExecuteResetAllRegistersCommand
            mStrings.Add(Names.ExecuteResetAllRegistersCommandName, "reset");
            mStrings.Add(Names.ExecuteResetAllRegistersCommandDescription, "Reset all of the registers to their initial values.");
            mStrings.Add(Names.ExecuteResetAllRegistersSuccessMessage, "Correctly reset all of the registers");
            #endregion

            #region ExecuteWriteFlagCommand
            mStrings.Add(Names.ExecuteWriteFlagCommandName, "[flag] [1|0]");
            mStrings.Add(Names.ExecuteWriteFlagCommandDescription, "Writes the specified bit to the specified flag of the register (f)");
            mStrings.Add(Names.ExecuteWriteFlagCommandSuccessMessage, "Flag {0} correctly set to {1}.");
            #endregion

            #region MemoryCommand
            mStrings.Add(Names.MemoryCommandName, "memory");
            mStrings.Add(Names.MemoryCommandDescription, "Allows operating directly with the device's memory.");
            mStrings.Add(Names.MemoryCommandPrompt, "memory");
            #endregion

            #region ReadMemoryCommand
            mStrings.Add(Names.ReadMemoryCommandName, "read");
            mStrings.Add(Names.ReadMemoryCommandDescription, "Allows reading from memory directly.");
            mStrings.Add(Names.ReadMemoryCommandPrompt, "memory-read");
            #endregion

            #region ExecuteReadMemoryCommand
            mStrings.Add(Names.ExecuteReadMemoryCommandName, "[address]");
            mStrings.Add(Names.ExecuteReadMemoryCommandDescription, "Reads data from the specified address.");
            mStrings.Add(Names.ExecuteReadMemoryCommandInvalidAddressMessage, "Invalid memory address. Valid addresses are within [0-{0}].");
            #endregion

            #region WriteMemoryCommand
            mStrings.Add(Names.WriteMemoryCommandName, "write");
            mStrings.Add(Names.WriteMemoryCommandDescription, "Allows writing to memory directly.");
            mStrings.Add(Names.WriteMemoryCommandPrompt, "memory-write");
            #endregion

            #region ExecuteWriteMemoryCommand
            mStrings.Add(Names.ExecuteWriteMemoryCommandName, "[address] [data]");
            mStrings.Add(Names.ExecuteWriteMemoryCommandDescription, "Writes data to the specified address.");
            mStrings.Add(Names.ExecuteWriteMemoryCommandInvalidAddressMessage, "Invalid memory address. Valid addresses are within [0-{0}].");
            mStrings.Add(Names.ExecuteWriteMemoryCommandInvalidDataMessage, "Invalid data. Valid data is within [0-{0}]");
            #endregion

            #region ClockCommand
            mStrings.Add(Names.ClockCommandName, "clock");
            mStrings.Add(Names.ClockCommandDescription, "Allows operating directly with the device's clock.");
            mStrings.Add(Names.ClockCommandPrompt, "clock");
            #endregion

            #region ClockTicksCommand
            mStrings.Add(Names.ClockTicksCommandName, "ticks");
            mStrings.Add(Names.ClockTicksCommandDescription, "Makes the clock run by ticks.");
            mStrings.Add(Names.ClockTicksCommandPrompt, "clock-ticks");
            #endregion

            #region ExecuteTicksCommand
            mStrings.Add(Names.ExecuteTicksCommandName, "[0-9]+");
            mStrings.Add(Names.ExecuteTicksCommandDescription, "Executes the specified number of ticks.");
            #endregion

            #region BreakpointsCommand
            mStrings.Add(Names.BreakpointsCommandName, "breakpoints");
            mStrings.Add(Names.BreakpointsCommandDescription, "Allows operating with breakpoints.");
            mStrings.Add(Names.BreakpointsCommandPrompt, "breakpoints");
            #endregion

            #region ExecuteListBreakpointsCommand
            mStrings.Add(Names.ExecuteListBreakpointsCommandName, "list");
            mStrings.Add(Names.ExecuteListBreakpointsCommandDescription, "Lists the available breakpoints.");
            mStrings.Add(Names.ExecuteListBreakpointsCommandNoBreakpointsErrorMessage, "There are no breakpoints!");
            #endregion

            #region AddBreakpointCommand
            mStrings.Add(Names.AddBreakpointCommandName, "add");
            mStrings.Add(Names.AddBreakpointCommandDescription, "Allows adding a new breakpoint.");
            mStrings.Add(Names.AddBreakpointCommandPrompt, "breakpoints-add");
            #endregion

            #region AddBreakpointByRegisterValueCommand
            mStrings.Add(Names.AddBreakpointByRegisterValueCommandName, "byregistervalue");
            mStrings.Add(Names.AddBreakpointByRegisterValueCommandDescription, "Adds a breakpoint that triggers when a register reaches a value.");
            mStrings.Add(Names.AddBreakpointByRegisterValueCommandPrompt, "breakpoints-add-byregistervalue");
            #endregion

            #region ExecuteAddBreakpointByRegisterValueCommand
            mStrings.Add(Names.ExecuteAddBreakpointByRegisterValueCommandName, "[register] [0-9]");
            mStrings.Add(Names.ExecuteAddBreakpointByRegisterValueCommandDescription, "Adds a breakpoint that triggers when the specified register reaches the specified value.");
            mStrings.Add(Names.ExecuteAddBreakpointByRegisterValueCommandSuccessMessage, "New breakpoint added!");
            mStrings.Add(Names.ExecuteAddBreakpointByRegisterValueCommandFailureMessage, "The breakpoint could not be created!");
            #endregion

            #region EnableBreakpointCommand
            mStrings.Add(Names.EnableBreakpointCommandName, "enable");
            mStrings.Add(Names.EnableBreakpointCommandDescription, "Enables a breakpoint.");
            mStrings.Add(Names.EnableBreakpointCommandPrompt, "breakpoints-enable");
            #endregion

            #region ExecuteEnableBreakpointCommand
            mStrings.Add(Names.ExecuteEnableBreakpointCommandName, "[0-9]+");
            mStrings.Add(Names.ExecuteEnableBreakpointCommandDescription, "Enables the breakpoint specified by index.");
            mStrings.Add(Names.ExecuteEnableBreakpointCommandSuccessMessage, "Breakpoint with index {0} correctly enabled.");
            mStrings.Add(Names.ExecuteEnableBreakpointCommandFailureMessage, "Could not enable breakpoint with index {0}.");
            #endregion

            #region DisableBreakpointCommand
            mStrings.Add(Names.DisableBreakpointCommandName, "disable");
            mStrings.Add(Names.DisableBreakpointCommandDescription, "Disables a breakpoint.");
            mStrings.Add(Names.DisableBreakpointCommandPrompt, "breakpoints-disable");
            #endregion

            #region ExecuteDisableBreakpointCommand
            mStrings.Add(Names.ExecuteDisableBreakpointCommandName, "[0-9]+");
            mStrings.Add(Names.ExecuteDisableBreakpointCommandDescription, "Disables the breakpoint specified by index.");
            mStrings.Add(Names.ExecuteDisableBreakpointCommandSuccessMessage, "Breakpoint with index {0} correctly disabled.");
            mStrings.Add(Names.ExecuteDisableBreakpointCommandFailureMessage, "Could not disable breakpoint with index {0}.");
            #endregion

            #region RemoveBreakpointCommand
            mStrings.Add(Names.RemoveBreakpointCommandName, "remove");
            mStrings.Add(Names.RemoveBreakpointCommandDescription, "Removes a breakpoint.");
            mStrings.Add(Names.RemoveBreakpointCommandPrompt, "breakpoints-remove");
            #endregion

            #region ExecuteRemoveBreakpointCommand
            mStrings.Add(Names.ExecuteRemoveBreakpointCommandName, "[0-9]+");
            mStrings.Add(Names.ExecuteRemoveBreakpointCommandDescription, "Removes the breakpoint specified by index.");
            mStrings.Add(Names.ExecuteRemoveBreakpointCommandSuccessMessage, "Breakpoint with index {0} correctly removed.");
            mStrings.Add(Names.ExecuteRemoveBreakpointCommandFailureMessage, "Could not remove breakpoint with index {0}.");
            #endregion

            #region ExecuteUntilBreakpointCommand
            mStrings.Add(Names.ExecuteRunUntilBreakpointCommandName, "run");
            mStrings.Add(Names.ExecuteRunUntilBreakpointCommandDescription, "Runs the emulator until a breakpoint triggers.");
            mStrings.Add(Names.ExecuteRunUntilBreakpointSuccessMessage, "Breakpoint with condition ({0}) triggered!");
            mStrings.Add(Names.ExecuteRunUntilBreakpointFailureMessage, "There are no breakpoints configured!");
            #endregion

            #region RegisterValueBreakpoint
            mStrings.Add(Names.RegisterValueBreakpointCondition, "{0} == {1}");
            mStrings.Add(Names.RegisterValueBreakpointDescription, "Triggers when register {0} reaches value {1}.");
            #endregion

            #region Error messages
            mStrings.Add(Names.InvalidRegisterErrorMessage, "Invalid register: {0}. Valid register names are ({1}).");
            #endregion
        }

        static void InitializeSpanish()
        {
            throw new NotImplementedException(
                "Spanish localization not implemented yet.");
        }

        static Dictionary<Names, string> mStrings;
        static bool mbInitialized = false;
    }
}
