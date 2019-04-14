using CommandLine;

namespace DotBoy.ConsoleRunner
{
    internal class Arguments
    {
        [Option(
            "trace",
            Default = false,
            HelpText = "Traces all of the execution through log. Forces to true if the 'debug' flag is present.")]
        public bool Trace { get; set; }

        [Option(
            "logfile",
            Default = Logging.NLOG_FILE,
            HelpText = "Sets the destination file of the logs.")]
        public string LogFile { get; set; }

        [Option(
            "loglayout",
            Default = Logging.NLOG_LAYOUT,
            HelpText = "Defines the log message layout.")]
        public string LogLayout { get; set; }

        [Option(
            "cycleaccurate",
            Default = false,
            HelpText = "Forces the emulator to run in a cycle-accurate manner.")]
        public bool AccurateCycles { get; set; }

        [Option(
            "failifcorrupted",
            Default = false,
            HelpText = "Forces the emulator to fail if the ROM doesn't pass Nintendo's integrity checks.")]
        public bool FailIfCorrupted { get; set; }

        [Option(
            "rom",
            Required = true,
            HelpText = "Loads the ROM located in the specified path.")]
        public string Rom { get; set; }

        [Option(
            "rominfo",
            Default = true,
            HelpText = "Prints the ROM information before beginning the execution.")]
        public bool PrintRomInfo { get; set; }

        [Option(
            "clockstep",
            Default = 0,
            HelpText = "Sets the minimum duration (in milliseconds) of the clock cycle.")]
        public long CpuClockStep { get; set; }

        [Option(
            "debug",
            Default = false,
            HelpText = "Starts the emulator in an interactive debugging session.")]
        public bool Debug { get; set; }
    }
}
