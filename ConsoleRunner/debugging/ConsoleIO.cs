using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ConsoleRunner.Debugging
{
    internal static class ConsoleIO
    {
        internal static string ReadStringInteractive(
            string prompt, ConsoleKey triggerKey, Action<string> interactiveAction)
        {
            Console.Write($"{prompt}> ");
            Encoding encoding = new UnicodeEncoding();

            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms, encoding))
            {
                int length = 0;

                ConsoleKeyInfo k;
                while ((k = Console.ReadKey()).Key != ConsoleKey.Enter)
                {
                    if (k.Key == triggerKey)
                    {
                        Console.Write(Environment.NewLine);
                        bw.Flush();

                        interactiveAction?.Invoke(
                            GetStringFromStream(ms, length, encoding));

                        RestorePrompt(prompt, ms, length, encoding);
                        continue;
                    }

                    if (k.Key == ConsoleKey.Backspace)
                    {
                        if (ms.Position == 0)
                        {
                            Console.Write(' ');
                            continue;
                        }

                        ms.Position -= sizeof(char);
                        length--;
                        Console.Write(" \b");
                        continue;
                    }

                    if (!IsPrintable(k.KeyChar))
                    {
                        Console.Write('\b');
                        continue;
                    }

                    length++;
                    bw.Write(k.KeyChar);
                }

                bw.Flush();
                return GetStringFromStream(ms, length, encoding);
            }
        }

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

        static void RestorePrompt(
            string prompt,
            MemoryStream content,
            int contentLength,
            Encoding encoding)
        {
            Console.Write($"{prompt}> ");
            Console.Write(GetStringFromStream(content, contentLength, encoding));
        }

        static string GetStringFromStream(
            MemoryStream ms, int strLength, Encoding encoding)
        {
            long oldPosition = ms.Position;
            ms.Position = 0;

            byte[] buffer = new byte[strLength * sizeof(char)];
            ms.Read(buffer, 0, buffer.Length);
            ms.Position = oldPosition;

            return encoding.GetString(buffer);
        }

        static bool IsPrintable(char c)
        {
            UnicodeCategory cat = char.GetUnicodeCategory(c);

            if (cat == UnicodeCategory.Control
                || cat == UnicodeCategory.Surrogate
                || cat == UnicodeCategory.OtherNotAssigned)
            {
                return false;
            }

            return true;
        }
    }
}
