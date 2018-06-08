using System;

using CommandLine;

using NLog;
using NLog.Targets;
using NLog.Config;

using DotBoy;
using DotBoy.Interfaces;
using ConsoleRunner.Debugging;

namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            CommandLine.Parser.Default.ParseArguments<Arguments>(args)
                .WithParsed<Arguments>(parsedArgs => RunProgram(parsedArgs));
        }

        static void RunProgram(Arguments args)
        {
            ConfigureLogging();

            Rom rom = RomLoader.Load(args.Rom, args.FailIfCorrupted);

            if (args.RomInfo)
                PrintRomInformation(rom.Information);

            if (args.Debug)
            {
                Debugger.RunDebugSession(rom, args.Trace, args.CpuClockStep);
                return;
            }

            var emulator = Emulator.InitForRegularRun(
                rom,
                new RealTimeSleeper(),
                args.Trace,
                args.CpuClockStep);

            emulator.Run();
        }

        static void ConfigureLogging()
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            consoleTarget.Layout = NLOG_LAYOUT;

            var fileTarget = new FileTarget();
            fileTarget.Layout = NLOG_LAYOUT;
            fileTarget.FileName = NLOG_FILE;
            fileTarget.KeepFileOpen = true;

            var consoleLoggingRule =
                new LoggingRule("*", LogLevel.Trace, consoleTarget);
            var fileLoggingRule =
                new LoggingRule("*", LogLevel.Trace, fileTarget);

            config.AddTarget("console", consoleTarget);
            config.AddTarget("file", fileTarget);

            config.LoggingRules.Add(consoleLoggingRule);
            config.LoggingRules.Add(fileLoggingRule);

            LogManager.Configuration = config;
        }

        static void PrintRomInformation(Rom.RomInformation information)
        {
            Console.WriteLine(
                $"Logo integrity correct: {information.IsLogoIntegrityCorrect}");

            Console.WriteLine(
                $"Header checksum correct: {information.IsHeaderChecksumCorrect}");

            Console.WriteLine(
                $"Cartridge checksum correct: {information.IsCartridgeChecksumCorrect}");

            Console.WriteLine(
                $"Platform: {information.Platform}");

            Console.WriteLine(
                $"Game title: {information.GameTitle}");

            Console.WriteLine(
                $"Licensee (Old code): {information.OldLicensee}");

            Console.WriteLine(
                $"Licensee (New code): {information.Licensee}");

            Console.WriteLine(
                $"Cartridge type: {information.Type}");

            Console.WriteLine(
                $"ROM size: {information.RomSize}");

            Console.WriteLine(
                $"RAM size: {information.RamSize}");

            Console.WriteLine(
                $"Destination code: {information.DestinationCode}");

            Console.WriteLine(
                $"Mask ROM Version number: {information.MaskRomVersionNumber}");

            Console.WriteLine(
                $"Complement check: {information.ComplementCheck}");

            Console.WriteLine(
                $"Checksum: {information.Checksum}");
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Environment.Exit(1);
        }

        class Arguments
        {
            [Option(
                't', "trace",
                Default = false,
                HelpText = "Traces all of the execution through log"
            )]
            public bool Trace { get; set; }

            [Option(
                'f', "failifcorrupted",
                Default = false,
                HelpText = "Fails if the ROM is corrupted."
            )]
            public bool FailIfCorrupted { get; set; }

            [Option(
                Required = true,
                HelpText = "Path of the ROM to be loaded."
            )]
            public string Rom { get; set; }

            [Option(
                'i', "rominfo",
                Default = true,
                HelpText = "Displays ROM information"
            )]
            public bool RomInfo { get; set; }

            [Option(
                "cpustep",
                Default = 0,
                HelpText = "Milliseconds between clock cycles")]
            public long CpuClockStep { get; set; }

            [Option(
                "debug",
                Default = false,
                HelpText = "Starts an interactive debugging session")]
            public bool Debug { get; set; }
        }

        const string USAGE = "Usage: ConsoleRunner.exe <Rom path>";

        const string NLOG_LAYOUT = @"${date:format=HH\:mm\:ss} ${logger} - ${message}";
        const string NLOG_FILE = "${basedir}/dotboy.log.txt";
    }
}
