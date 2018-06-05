using System;
using System.Reflection;

using DotBoy.Utils;

namespace DotBoy
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
        _1MB = 0x05,   // only 63 banks used by MBC1
        _2MB = 0x06,   // only 125 banks used by MBC1
        _4MB = 0x07,
        _8MB = 0x08,
        _1p1MB = 0x52,
        _1p2MB = 0x53,
        _1p5MB = 0x54
    }

    public enum RamSizeId : byte
    {
        None = 0x00,
        _2KB = 0x01,
        _8KB = 0x02,
        _32KB = 0x03,  // 4 banks of 8KBytes each
        _128KB = 0x04, // 16 banks of 8KBytes each
        _64KB = 0x05   // 8 banks of 8KBytes each
    }

    // From http://gbdev.gg8.se/wiki/articles/Gameboy_ROM_Header_Info#Licensee
    public enum OldLicenseeCodeId : byte
    {
        [StringValue("none")]
        none = 0x00,

        [StringValue("nintendo")]
        nintendo_a = 0x01,

        [StringValue("capcom")]
        capcom_a = 0x08,

        [StringValue("hot-b")]
        hot_b = 0x09,

        [StringValue("jaleco")]
        jaleco_a = 0x0A,

        [StringValue("coconuts")]
        coconuts = 0x0B,

        [StringValue("elite systems")]
        elite_systems_a = 0x0C,

        [StringValue("electronic arts")]
        electronic_arts_a = 0x13,

        [StringValue("hudsonsoft")]
        hudsonsoft = 0x18,

        [StringValue("itc entertainment")]
        itc_entertainment = 0x19,

        [StringValue("yanoman")]
        yanoman = 0x1A,

        [StringValue("clary")]
        clary = 0x1D,

        [StringValue("virgin")]
        virgin_a = 0x1F,

        [StringValue("pcm complete")]
        pcm_complete = 0x24,

        [StringValue("san-x")]
        san_x = 0x25,

        [StringValue("kotobuki systems")]
        kotobuki_systems = 0x28,

        [StringValue("seta")]
        seta = 0x29,

        [StringValue("infogrames")]
        infogrames_a = 0x30,

        [StringValue("nintendo")]
        nintendo_b = 0x31,

        [StringValue("bandai")]
        bandai_a = 0x32,

        [StringValue("GBC")]
        GBC = 0x33,

        [StringValue("konami")]
        konami_a = 0x34,

        [StringValue("hector")]
        hector = 0x35,

        [StringValue("capcom")]
        capcom_b = 0x38,

        [StringValue("banpresto")]
        banpresto_a = 0x39,

        [StringValue("*entertainment i")]
        entertainment_i = 0x3C,

        [StringValue("gremlin")]
        gremlin = 0x3E,

        [StringValue("ubi soft")]
        ubi_soft = 0x41,

        [StringValue("atlus")]
        atlus_a = 0x42,

        [StringValue("malibu")]
        malibu_a = 0x44,

        [StringValue("angel")]
        angel_a = 0x46,

        [StringValue("spectrum holoby")]
        spectrum_holoby = 0x47,

        [StringValue("irem")]
        irem = 0x49,

        [StringValue("virgin")]
        virgin_b = 0x4A,

        [StringValue("malibu")]
        malibu_b = 0x4D,

        [StringValue("u.s. gold")]
        us_gold = 0x4F,

        [StringValue("absolute")]
        absolute = 0x50,

        [StringValue("acclaim")]
        acclaim_a = 0x51,

        [StringValue("activision")]
        activision = 0x52,

        [StringValue("american sammy ")]
        american_sammy = 0x53,

        [StringValue("gametek")]
        gametek = 0x54,

        [StringValue("park place")]
        park_place = 0x55,

        [StringValue("ljn")]
        ljn_a = 0x56,

        [StringValue("matchbox")]
        matchbox = 0x57,

        [StringValue("milton bradley")]
        milton_bradley = 0x59,

        [StringValue("mindscape")]
        mindscape = 0x5A,

        [StringValue("romstar")]
        romstar = 0x5B,

        [StringValue("naxat soft")]
        naxat_soft_a = 0x5C,

        [StringValue("tradewest")]
        tradewest = 0x5D,

        [StringValue("titus")]
        titus = 0x60,

        [StringValue("virgin")]
        virgin_c = 0x61,

        [StringValue("ocean")]
        ocean = 0x67,

        [StringValue("electronic arts")]
        electronic_arts_b = 0x69,

        [StringValue("elite systems")]
        elite_systems_b = 0x6E,

        [StringValue("electro brain")]
        electro_brain = 0x6F,

        [StringValue("infogrames")]
        infogrames_b = 0x70,

        [StringValue("interplay")]
        interplay = 0x71,

        [StringValue("broderbund")]
        broderbund_a = 0x72,

        [StringValue("sculptered soft")]
        sculptered_soft = 0x73,

        [StringValue("the sales curve")]
        the_sales_curve = 0x75,

        [StringValue("t*hq")]
        thq = 0x78,

        [StringValue("accolade")]
        accolade = 0x79,

        [StringValue("triffix entertainment")]
        triffix_entertainment = 0x7A,

        [StringValue("microprose")]
        microprose = 0x7C,

        [StringValue("kemco")]
        kemco_a = 0x7F,

        [StringValue("misawa entertainment")]
        misawa_entertainment = 0x80,

        [StringValue("lozc")]
        lozc = 0x83,

        [StringValue("tokuma shoten intermedia")]
        tokuma_shoten_intermedia_a = 0x86,

        [StringValue("bullet-proof software")]
        bullet_proof_software = 0x8B,

        [StringValue("vic tokai")]
        vic_tokai = 0x8C,

        [StringValue("ape")]
        ape = 0x8E,

        [StringValue("i'max")]
        imax = 0x8F,

        [StringValue("chun soft")]
        chun_soft = 0x91,

        [StringValue("video system")]
        video_system = 0x92,

        [StringValue("tsuburava")]
        tsuburava = 0x93,

        [StringValue("varie")]
        varie_a = 0x95,

        [StringValue("yonezawa/s'pal")]
        yonezawaspal = 0x96,

        [StringValue("kaneko")]
        kaneko = 0x97,

        [StringValue("arc")]
        arc = 0x99,

        [StringValue("nihon bussan")]
        nihon_bussan = 0x9A,

        [StringValue("tecmo")]
        tecmo = 0x9B,

        [StringValue("imagineer")]
        imagineer = 0x9C,

        [StringValue("banpresto")]
        banpresto_b = 0x9D,

        [StringValue("nova")]
        nova = 0x9F,

        [StringValue("hori electric")]
        hori_electric = 0xA1,

        [StringValue("bandai")]
        bandai_b = 0xA2,

        [StringValue("konami")]
        konami_b = 0xA4,

        [StringValue("kawada")]
        kawada = 0xA6,

        [StringValue("takara")]
        takara = 0xA7,

        [StringValue("technos japan")]
        technos_japan = 0xA9,

        [StringValue("broderbund")]
        broderbund_b = 0xAA,

        [StringValue("toei animation")]
        toei_animation = 0xAC,

        [StringValue("toho")]
        toho = 0xAD,

        [StringValue("namco")]
        namco = 0xAF,

        [StringValue("acclaim")]
        acclaim_b = 0xB0,

        [StringValue("ascii or nexoft")]
        ascii_or_nexoft = 0xB1,

        [StringValue("bandai")]
        bandai_c = 0xB2,

        [StringValue("enix")]
        enix = 0xB4,

        [StringValue("hal")]
        hal = 0xB6,

        [StringValue("snk")]
        snk = 0xB7,

        [StringValue("pony canyon")]
        pony_canyon = 0xB9,

        [StringValue("*culture brain o")]
        culture_brain_o = 0xBA,

        [StringValue("sunsoft")]
        sunsoft = 0xBB,

        [StringValue("sony imagesoft")]
        sony_imagesoft = 0xBD,

        [StringValue("sammy")]
        sammy = 0xBF,

        [StringValue("taito")]
        taito_a = 0xC0,

        [StringValue("kemco")]
        kemco_b = 0xC2,

        [StringValue("squaresoft")]
        squaresoft = 0xC3,

        [StringValue("tokuma shoten intermedia")]
        tokuma_shoten_intermedia_b = 0xC4,

        [StringValue("data east")]
        data_east = 0xC5,

        [StringValue("tonkin house")]
        tonkin_house = 0xC6,

        [StringValue("koei")]
        koei = 0xC8,

        [StringValue("ufl")]
        ufl = 0xC9,

        [StringValue("ultra")]
        ultra = 0xCA,

        [StringValue("vap")]
        vap = 0xCB,

        [StringValue("use")]
        use = 0xCC,

        [StringValue("meldac")]
        meldac = 0xCD,

        [StringValue("*pony canyon or")]
        pony_canyon_or = 0xCE,

        [StringValue("angel")]
        angel_b = 0xCF,

        [StringValue("taito")]
        taito_b = 0xD0,

        [StringValue("sofel")]
        sofel = 0xD1,

        [StringValue("quest")]
        quest = 0xD2,

        [StringValue("sigma enterprises")]
        sigma_enterprises = 0xD3,

        [StringValue("ask kodansha")]
        ask_kodansha = 0xD4,

        [StringValue("naxat soft")]
        naxat_soft_b = 0xD6,

        [StringValue("copya systems")]
        copya_systems = 0xD7,

        [StringValue("banpresto")]
        banpresto = 0xD9,

        [StringValue("tomy")]
        tomy = 0xDA,

        [StringValue("ljn")]
        ljn_b = 0xDB,

        [StringValue("ncs")]
        ncs = 0xDD,

        [StringValue("human")]
        human = 0xDE,

        [StringValue("altron")]
        altron = 0xDF,

        [StringValue("jaleco")]
        jaleco_b = 0xE0,

        [StringValue("towachiki")]
        towachiki = 0xE1,

        [StringValue("uutaka")]
        uutaka = 0xE2,

        [StringValue("varie")]
        varie_b = 0xE3,

        [StringValue("epoch")]
        epoch = 0xE5,

        [StringValue("athena")]
        athena = 0xE7,

        [StringValue("asmik")]
        asmik = 0xE8,

        [StringValue("natsume")]
        natsume = 0xE9,

        [StringValue("king records")]
        king_records = 0xEA,

        [StringValue("atlus")]
        atlus_b = 0xEB,

        [StringValue("epic/sony records")]
        epicsony_records = 0xEC,

        [StringValue("igs")]
        igs = 0xEE,

        [StringValue("a wave")]
        a_wave = 0xF0,

        [StringValue("extreme entertainment")]
        extreme_entertainment = 0xF3,

        [StringValue("ljn")]
        ljn_c = 0xFF
    }

    // From http://gbdev.gg8.se/wiki/articles/The_Cartridge_Header#0148_-_ROM_Size
    // Two ASCII characters transformed to ushort
    public enum LicenseeCodeId : ushort
    {
        [StringValue("None")]
        None = 0x3030,

        [StringValue("Nintendo R&D1")]
        NintendoRandD1 = 0x3031,

        [StringValue("Capcom")]
        Capcom = 0x3038,

        [StringValue("Electronics Arts")]
        ElectronicArts_A = 0x3133,

        [StringValue("Hudson Soft")]
        HudsonSoft = 0x3136,

        [StringValue("b-ai")]
        b_ai = 0x3139,

        [StringValue("kss")]
        kss = 0x3230,

        [StringValue("pow")]
        pow = 0x3232,

        [StringValue("PCM Complete")]
        PCMComplete = 0x3234,

        [StringValue("san-x")]
        san_x = 0x3235,

        [StringValue("Kemco Japan")]
        KemcoJapan = 0x3236,

        [StringValue("seta")]
        seta = 0x3239,

        [StringValue("Viacom")]
        Viacom = 0x3330,

        [StringValue("Nintendo")]
        Nintendo = 0x3331,

        [StringValue("Bandai")]
        Bandai = 0x3332,

        [StringValue("Ocean/Acclaim")]
        Ocean_Acclaim_A = 0x3333,

        [StringValue("Konami")]
        Konami_A = 0x3334,

        [StringValue("Hector")]
        Hector = 0x3335,

        [StringValue("Taito")]
        Taito = 0x3337,

        [StringValue("Hudson")]
        Hudson = 0x3338,

        [StringValue("Banpresto")]
        Banpresto = 0x3339,

        [StringValue("Ubi Soft")]
        UbiSoft = 0x3431,

        [StringValue("Atlus")]
        Atlus = 0x3432,

        [StringValue("Malibu")]
        Malibu = 0x3434,

        [StringValue("angel")]
        angel = 0x3436,

        [StringValue("Bullet-Proof")]
        Bullet_Proof = 0x3437,

        [StringValue("item")]
        irem = 0x3439,

        [StringValue("Absolute")]
        Absolute = 0x3530,

        [StringValue("Acclaim")]
        Acclaim = 0x3531,

        [StringValue("Activision")]
        Activision = 0x3532,

        [StringValue("American sammy")]
        AmericanSammy = 0x3533,

        [StringValue("Konami")]
        Konami_B = 0x3534,

        [StringValue("Hi tech entertainment")]
        Hi_tech_entertainment = 0x3535,

        [StringValue("LJN")]
        LJN = 0x3536,

        [StringValue("Matchbox")]
        Matchbox = 0x3537,

        [StringValue("Mattel")]
        Mattel = 0x3538,

        [StringValue("Milton Bradley")]
        MiltonBradley = 0x3539,

        [StringValue("Titus")]
        Titus = 0x3630,

        [StringValue("Virgin")]
        Virgin = 0x3631,

        [StringValue("LucasArts")]
        LucasArts = 0x3634,

        [StringValue("Ocean")]
        Ocean = 0x3637,

        [StringValue("Electronic Arts")]
        ElectronicArts_B = 0x3639,

        [StringValue("Infogrames")]
        Infogrames = 0x3730,

        [StringValue("Interplay")]
        Interplay = 0x3731,

        [StringValue("Broderbund")]
        Broderbund = 0x3732,

        [StringValue("sculptured")]
        sculptured = 0x3733,

        [StringValue("sci")]
        sci = 0x3735,

        [StringValue("THQ")]
        THQ = 0x3738,

        [StringValue("Accolade")]
        Accolade = 0x3739,

        [StringValue("misawa")]
        misawa = 0x3830,

        [StringValue("lozc")]
        lozc = 0x3833,

        [StringValue("tokuma shoten i*")]
        tokuma_shoten_i = 0x3836,

        [StringValue("tsukuda ori*")]
        tsukuda_ori = 0x3837,

        [StringValue("Chunsoft")]
        Chunsoft = 0x3931,

        [StringValue("Video system")]
        Video_system = 0x3932,

        [StringValue("Ocean/Acclaim")]
        Ocean_Acclaim_2 = 0x3933,

        [StringValue("Varie")]
        Varie = 0x3935,

        [StringValue("Yonezawa/s'pal")]
        Yonezawas_pal = 0x3936,

        [StringValue("Kaneko")]
        Kaneko = 0x3937,

        [StringValue("Pack in soft")]
        Pack_in_soft = 0x3939,

        [StringValue("Konami (Yu-Gi-Oh!)")]
        Konami_YuGiOh = 0x4134
    }

    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            string output = string.Empty;
            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());
            if (fieldInfo == null)
                return output;

            StringValue[] attrs = fieldInfo.GetCustomAttributes(
                typeof(StringValue), false) as StringValue[];

            if (attrs.Length > 0)
                output = attrs[0].Value;

            return output;
        }
    }
}
