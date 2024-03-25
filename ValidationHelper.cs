using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bus_station
{
    // three functions --------------------------?
    static class ValidationHelper
    {
        public static int GetValidUserChoice(int minValue, int maxValue)
        {
            int userInput;

            do
            {
                Console.Write("\nEnter your choice ({0}-{1}): ", minValue, maxValue);
                userInput = GetValidIntegerInput(); // validate type

                //validate range
                if (userInput < minValue || userInput > maxValue)
                {
                    ConsoleUtils.DisplayErrorMessage($"Invalid input. Please enter a number between {minValue} and {maxValue}.\n");

                }

            } while (userInput < minValue || userInput > maxValue);

            return userInput;
        }

        public static int GetValidIntegerInput()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                ConsoleUtils.DisplayErrorMessage("Invalid input. Please enter a valid integer.");
            }
            return input;
        }

        public static double GetValidDoubleInput()
        {
            double input;
            while (!double.TryParse(Console.ReadLine(), out input))
            {
                ConsoleUtils.DisplayErrorMessage("Invalid input. Please enter a valid double.");
            }
            return input;
        }

        public static string GetNonEmptyStringInput()
        {
            string input;
            do
            {
                input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    ConsoleUtils.DisplayErrorMessage("Invalid input. Name cannot be empty. Please try again.");
                }
            } while (string.IsNullOrEmpty(input));

            return input;
        }
        public static string getNameInput()
        {
            string name;
            do
            {
                name = GetNonEmptyStringInput();
                if (!Regex.Match(name, "^([a-z]{3,}\\s?)([a-z]{3,}\\s?)*(([a-z]{3,}\\s?)|)$").Success)
                {
                    Console.WriteLine("please enter the official name");
                }
            } while (!Regex.Match(name, "^([a-z]{3,}\\s?)([a-z]{3,}\\s?)*(([a-z]{3,}\\s?)|)$").Success);
            return name;
        }
    }
}
