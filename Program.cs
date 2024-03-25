using System;

namespace bus_station
{
    class Program
    {
        public static void Main(string[] args)
        {
            ProgramFollow.DisplayMainMenu(ConsoleColor.DarkCyan);
            int userInput = ValidationHelper.GetValidUserChoice(1, 4);
            ProgramFollow.HandleUserChoice(userInput);


            //if (int.TryParse(userInput, out int userChoice))
            //{
            //    HandleUserChoice(userChoice);
            //}
            //else
            //{
            //    Console.WriteLine("Invalid input. Please enter a number.");
            //}

            //tsql user1 = new tsql("test1","wwwwwwwwwwwwwwwwwwwww");

            //user1.query();
            //user1.Dispose();






            //Console.WriteLine("\n\n\npress anykey to exit.");
            //Console.ReadKey();
        }

        
    }
}
