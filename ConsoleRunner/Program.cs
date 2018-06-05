using System;

using NLog;
using NLog.Targets;
using NLog.Config;

using DotBoy;

namespace ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine(USAGE);
                Environment.Exit(1);
            }

            ConfigureLogging();

            Rom rom = RomLoader.Load(args[0], failIfCorrupted: false);
            PrintRomInformation(rom.Information);

            var emulator = Emulator.Init(rom, new RealTimeSleeper());
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

        const string USAGE = "Usage: ConsoleRunner.exe <Rom path>";

        const string NLOG_LAYOUT = @"${date:format=HH\:mm\:ss} ${logger} - ${message}";
        const string NLOG_FILE = "${basedir}/dotboy.log.txt";
    }
}
