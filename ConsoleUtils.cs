using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_station
{
    public class ConsoleUtils
    {
        public static void DisplayHeader(string header, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.Write(new string('=', Console.WindowWidth));
            CenterText(header);
            Console.Write(new string('=', Console.WindowWidth));

            Console.ResetColor();
        }
        public static void CenterText(string text)
        {
            int windowWidth = Console.WindowWidth;
            Console.SetCursorPosition((windowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }

        public static void DrawMenuTable(string header, string[] options)
        {
            int MaxLength = GetMaxStringLength(header, options);
            DrawTableHeader(header, MaxLength);
            DrawTableContent(options, MaxLength);
        }

        public static void DrawTableHeader(string header, int MaxLength)
        {
            Console.WriteLine("+" + new string('-', MaxLength + 2) + "+");
            Console.WriteLine("| " + header.PadRight(MaxLength) + " |");
            Console.WriteLine("+" + new string('-', MaxLength + 2) + "+");
        }

        public static void DrawTableContent(string[] options, int optionMaxLength)
        {
            foreach (var option in options)
            {
                Console.WriteLine("| " + option.PadRight(optionMaxLength) + " |");
            }

            Console.WriteLine("+" + new string('-', optionMaxLength + 2) + "+");
        }

        private static int GetMaxStringLength(string header, string[] options)
        {
            int maxLength = header.Length;
            for (int i = 0; i < options.Length; i++)
            {
                maxLength = Math.Max(maxLength, options[i].Length);
            }

            return maxLength;
        }

        public static void DisplayErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void DisplaySuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
