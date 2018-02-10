using System;

using SharpBoy.Utils;

namespace SharpBoy
{
    public class Rom
    {
        public byte[] Content { get => mContent; }
        public RomInformation Information { get => mRomInformation; }

        public Rom(byte[] content, RomInformation information)
        {
            mContent = content;
            mRomInformation = information;
        }

        readonly byte[] mContent;
        readonly RomInformation mRomInformation;

        public class RomInformation
        {
            public enum PlatformId : byte
            {
                GameBoy = 0x00,
                SuperGameBoy = 0x03,
                GameBoyColor = 0x80
            }

            public enum DestinationCodeId : byte
            {
                Japanese = 0x00,
                NonJapanese = 0x01
            }

            public enum CartridgeTypeId : byte
            {
                RomOnly = 0x00,
                RomMbc1 = 0x01,
                RomMbc1Ram = 0x02,
                RomMbc1RamBatt = 0x03,
                RomMbc2 = 0x05,
                RomMbc2Batt = 0x06,
                RomRam = 0x08,
                RomRamBatt = 0x09,
                RomMmmo1 = 0x0B,
                RomMmmo1Sram = 0x0C,
                RomMmmo1SramBatt = 0x0D,
                RomMbc3TimerBatt = 0x0F,
                RomMbc3TimerRamBatt = 0x10,
                RomMbc3 = 0x11,
                RomMbc3Ram = 0x12,
                RomMbc3RamBatt = 0x13,
                RomMbc5 = 0x19,
                RomMbc5Ram = 0x1A,
                RomMbc5RamBatt = 0x1B,
                RomMbc5Rumble = 0x1C,
                RomMbc5RumbleSram = 0x1D,
                RomMbc5RumbleSramBatt = 0x1E,
                PocketCamera = 0xFD,
                HudsonHuC3 = 0xFE,
                HudsonHuC1 = 0xFF
            }

            public enum RomSizeId : byte
            {
                _32KB = 0x00,
                _64KB = 0x01,
                _128KB = 0x02,
                _256KB = 0x03,
                _512KB = 0x04,
                _1MB = 0x05,
                _2MB = 0x06,
                _1p1MB = 0x52,
                _1p2MB = 0x53,
                _1p5MB = 0x54
            }

            public enum RamSizeId : byte
            {
                None = 0x00,
                _2KB = 0x01,
                _8KB = 0x02,
                _32KB = 0x03,
                _128KB = 0x04
            }

            public enum LicenseCodeId : byte
            {
                CheckElsewhere = 0x33,
                Accolae = 0x79,
                Konami = 0xA4
            }

            public bool IsLogoIntegrityCorrect { get; private set; }
            public bool IsHeaderChecksumCorrect { get; private set; }
            public bool IsCartridgeChecksumCorrect { get; private set; }

            public PlatformId Platform { get; private set; }
            public string GameTitle { get; private set; }

            public byte LicenseCodeHighNibble { get; private set; }
            public byte LicenseCodeLowNibble { get; private set; }

            public CartridgeTypeId Type { get; private set; }
            public RomSizeId RomSize { get; private set; }
            public RamSizeId RamSize { get; private set; }
            public DestinationCodeId DestinationCode { get; private set; }
            public LicenseCodeId LicenseCode { get; private set; }

            public byte MaskRomVersionNumber { get; private set; }
            public byte ComplementCheck { get; private set; }
            public ushort Checksum { get; private set; }

            public bool IntegrityCheckPassed
            {
                get => IsLogoIntegrityCorrect
                      && IsHeaderChecksumCorrect
                      && IsCartridgeChecksumCorrect;
            }

            public static RomInformation Initialize(byte[] romContent)
            {
                bool isLogoIntegrityCorrect = ByteArray.Equals(
                    romContent,
                    0x0104,
                    NINTENDO_LOGO,
                    0,
                    0x0133 - 0x0104 + 1);

                bool isHeaderChecksumCorrect =
                    CheckHeaderChecksum(romContent, 0x014D);

                bool isCartridgeChecksumCorrect =
                    CheckCartridgeChecksum(romContent, 0x014E, 0x014F);

                PlatformId platform = CalculatePlatform(
                    romContent[0x0146], romContent[0x0143]);

                string gameTitle = ByteArray.GetAsciiString(
                    romContent, 0x0134, 0x0142 - 0x0134 + 1);

                byte licenseCodeHighNibble = romContent[0x0144];
                byte licenseCodeLowNibble = romContent[0x0145];

                CartridgeTypeId type = (CartridgeTypeId)romContent[0x0147];
                RomSizeId romSize = (RomSizeId)romContent[0x0148];
                RamSizeId ramSize = (RamSizeId)romContent[0x0149];
                DestinationCodeId destinationCode = (DestinationCodeId)romContent[0x014A];
                LicenseCodeId licenseCode = (LicenseCodeId)romContent[0x014B];

                byte maskRomVersionNumber = romContent[0x014C];
                byte complementCheck = romContent[0x014D];
                ushort checksum = BitConverter.ToUInt16(romContent, 0x014E);

                return new RomInformation()
                {
                    IsLogoIntegrityCorrect = isLogoIntegrityCorrect,
                    IsHeaderChecksumCorrect = isHeaderChecksumCorrect,
                    IsCartridgeChecksumCorrect = isCartridgeChecksumCorrect,
                    Platform = platform,
                    GameTitle = gameTitle,
                    LicenseCodeHighNibble = licenseCodeHighNibble,
                    LicenseCodeLowNibble = licenseCodeLowNibble,
                    Type = type,
                    RomSize = romSize,
                    RamSize = ramSize,
                    DestinationCode = destinationCode,
                    LicenseCode = licenseCode,
                    MaskRomVersionNumber = maskRomVersionNumber,
                    ComplementCheck = complementCheck,
                    Checksum = checksum
                };
            }

#warning Untested method
            static PlatformId CalculatePlatform(
                byte gbSgbIndicator, byte gbGbcIndicator)
            {
                if (gbSgbIndicator == (byte)PlatformId.SuperGameBoy)
                    return PlatformId.SuperGameBoy;

                if (gbGbcIndicator == (byte)PlatformId.GameBoy)
                    return PlatformId.GameBoy;

                return PlatformId.GameBoyColor;
            }

#warning untested method
            static bool CheckCartridgeChecksum(
                byte[] romContent,
                ushort hChecksumAddress,
                ushort lChecksumAddress)
            {
                uint checksum = CartridgeChecksum.CalculateChecksum(
                    romContent,
                    0,
                    romContent.Length,
                    hChecksumAddress,
                    lChecksumAddress);

                byte[] checksumBytes = BitConverter.GetBytes(checksum);
                return checksumBytes[3] == romContent[hChecksumAddress]
                    && checksumBytes[2] == romContent[lChecksumAddress];
            }

#warning untested method
            static bool CheckHeaderChecksum(
                byte[] romContent, ushort checksumAddress)
            {
                int checksum = CartridgeChecksum.CalculateHeaderChecksum(
                    romContent,
                    0x0134,
                    0x014C - 0x0134 + 1);

                byte[] checksumBytes = BitConverter.GetBytes(checksum);
                return checksumBytes[0] == romContent[checksumAddress];
            }

            static readonly byte[] NINTENDO_LOGO =
            {
                0xCE, 0xED, 0x66, 0x66, 0xCC, 0x0D, 0x00, 0x0B,
                0x03, 0x73, 0x00, 0x83, 0x00, 0x0C, 0x00, 0x0D,
                0x00, 0x08, 0x11, 0x1F, 0x88, 0x89, 0x00, 0x0E,
                0xDC, 0xCC, 0x6E, 0xE6, 0xDD, 0xDD, 0xD9, 0x99,
                0xBB, 0xBB, 0x67, 0x63, 0x6E, 0x0E, 0xEC, 0xCC,
                0xDD, 0xDC, 0x99, 0x9F, 0xBB, 0xB9, 0x33, 0x3E
            };
        }
    }
}
