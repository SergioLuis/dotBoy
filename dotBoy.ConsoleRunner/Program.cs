using System;

using CommandLine;

using NLog;
using NLog.Targets;
using NLog.Config;

using DotBoy.Core;
using DotBoy.Core.Emulation;
using DotBoy.Debugging;

namespace DotBoy.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            Parser.Default.ParseArguments<Arguments>(args)
                .WithParsed(parsedArgs => RunProgram(parsedArgs));
        }

        static void RunProgram(Arguments args)
        {
            Logging.TryConfigure(args.LogFile, args.LogLayout);
            Localization.TryInitialize(Localization.Language.English);

            Rom rom = RomLoader.Load(args.Rom, args.FailIfCorrupted);

            if (args.PrintRomInfo)
                PrintRomInformation(rom.Information);

            if (args.Debug)
            {
                Debugger.RunDebugSession(
                    rom,
                    new DefaultSleeper(),
                    args.Trace,
                    args.AccurateCycles,
                    args.CpuClockStep);

                return;
            }

            var emulator = Emulator.InitForRegularRun(
                rom,
                new DefaultSleeper(),
                args.Trace,
                args.AccurateCycles,
                args.CpuClockStep);

            emulator.Run();
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

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Environment.Exit(1);
        }

        const string USAGE = "Usage: ConsoleRunner.exe <Rom path>";
    }
}
