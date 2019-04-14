using System;

using DotBoy.Core.Utils;

namespace DotBoy.Core
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
            public bool IsLogoIntegrityCorrect { get; private set; }
            public bool IsHeaderChecksumCorrect { get; private set; }
            public bool IsCartridgeChecksumCorrect { get; private set; }

            public PlatformId Platform { get; private set; }
            public string GameTitle { get; private set; }

            public string OldLicensee { get => OldLicenseeCode.GetStringValue(); }
            public OldLicenseeCodeId OldLicenseeCode { get; private set; }

            public string Licensee { get => LicenseeCode.GetStringValue(); }
            public LicenseeCodeId LicenseeCode { get; private set; }

            public CartridgeTypeId Type { get; private set; }
            public RomSizeId RomSize { get; private set; }
            public RamSizeId RamSize { get; private set; }
            public DestinationCodeId DestinationCode { get; private set; }

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

                LicenseeCodeId licenseeCode = (LicenseeCodeId) BitConverter.ToUInt16(
                    new byte[] { romContent[0x0145], romContent[0x0144] }, 0);

                CartridgeTypeId type = (CartridgeTypeId)romContent[0x0147];
                RomSizeId romSize = (RomSizeId)romContent[0x0148];
                RamSizeId ramSize = (RamSizeId)romContent[0x0149];
                DestinationCodeId destinationCode = (DestinationCodeId)romContent[0x014A];
                OldLicenseeCodeId oldLicenseeCode = (OldLicenseeCodeId)romContent[0x014B];

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
                    OldLicenseeCode = oldLicenseeCode,
                    LicenseeCode = licenseeCode,
                    Type = type,
                    RomSize = romSize,
                    RamSize = ramSize,
                    DestinationCode = destinationCode,
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
