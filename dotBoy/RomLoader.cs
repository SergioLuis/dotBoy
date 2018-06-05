using System.IO;

namespace DotBoy
{
    public class RomLoader
    {
        public static Rom Load(string path, bool failIfCorrupted)
        {
            byte[] content;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                content = new byte[fs.Length];
                fs.Read(content, 0, content.Length);
            }

            return Load(content, failIfCorrupted);
        }

        public static Rom Load(byte[] romContent, bool failIfCorrupted)
        {
            Rom result = new Rom(
                romContent, Rom.RomInformation.Initialize(romContent));

            if (!failIfCorrupted)
                return result;

            // TODO integrity checks;
            return result;
        }
    }
}
