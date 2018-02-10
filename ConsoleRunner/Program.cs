using System;

using SharpBoy;

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

            Rom rom = RomLoader.Load(args[0], failIfCorrupted: false);
            PrintRomInformation(rom.Information);
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
                $"License code (HN): {information.LicenseCodeHighNibble}");

            Console.WriteLine(
                $"License code (LN): {information.LicenseCodeLowNibble}");

            Console.WriteLine(
                $"Cartridge type: {information.Type}");

            Console.WriteLine(
                $"ROM size: {information.RomSize}");

            Console.WriteLine(
                $"RAM size: {information.RamSize}");

            Console.WriteLine(
                $"Destination code: {information.DestinationCode}");

            Console.WriteLine(
                $"Licensee code: {information.LicenseeCode}");

            Console.WriteLine(
                $"Mask ROM Version number: {information.MaskRomVersionNumber}");

            Console.WriteLine(
                $"Complement check: {information.ComplementCheck}");

            Console.WriteLine(
                $"Checksum: {information.Checksum}"
                );
    }

        const string USAGE = "Usage: ConsoleRunner.exe <Rom path>";
    }
}
