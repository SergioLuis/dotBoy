using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRunner.Debugging
{
    internal static class ConsoleIO
    {
        internal static string ReadString(string prompt)
        {
            Console.Write($"{prompt}> ");
            return Console.ReadLine();
        }

        internal static void WriteLine(string line)
        {
            Console.WriteLine($" {line}");
        }

        internal static string FormatNumber(byte value)
        {
            return string.Format(
                "{0} / 0x{0:X2} / {1}",
                value,
                Convert.ToString(value, 2).PadLeft(8, '0'));
        }

        internal static string FormatNumber(ushort value)
        {
            return string.Format(
                "{0} / 0x{0:X2} / {1}",
                value,
                Convert.ToString(value, 2).PadLeft(16, '0'));
        }

        internal static string[] Split(string str)
        {
            var sb = new StringBuilder();
            var result = new List<string>();

            bool insideQuotes = false;
            char quotesCharacter = '\'';
            foreach (char c in str)
            {
                if ((c == '"' || c == '\''))
                {
                    if (!insideQuotes)
                    {
                        insideQuotes = true;
                        quotesCharacter = c;
                        continue;
                    }

                    if (c == quotesCharacter)
                    {
                        result.Add(sb.ToString());
                        sb = new StringBuilder();
                        insideQuotes = false;
                        continue;
                    }

                    sb.Append(c);
                    continue;
                }

                if (c == ' ')
                {
                    if (!insideQuotes)
                    {
                        if (string.IsNullOrEmpty(sb.ToString()))
                            continue;

                        result.Add(sb.ToString());
                        sb = new StringBuilder();
                        continue;
                    }

                    sb.Append(c);
                    continue;
                }

                sb.Append(c);
            }

            string finalStr = sb.ToString();
            if (!string.IsNullOrEmpty(finalStr))
                result.Add(finalStr);

            return result.ToArray();
        }
    }
}
