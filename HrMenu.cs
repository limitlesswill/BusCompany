using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_station
{
    public class HrMenu
    {
        enum HROptions
        {
            Stops = 1,
            Employees = 2,
            Drivers = 3,
            Back = 4
        }

        enum Options
        {
            Add = 1,
            Show = 2,
            Update = 3,
            Delete = 4,
            Back = 5
        }

        public static void DisplayHROptions(ConsoleColor color)
        {
            Console.Clear();
            ConsoleUtils.DisplayHeader("BUS STATION SYSTEM", color);
            Console.ForegroundColor = color;

            ConsoleUtils.DrawMenuTable("HR Operations", new string[] {
                "1. Stops options",
                "2. Employees options",
                "3. Drivers options",
                "4. Go Back"
            });

            Console.ResetColor();
        }

        static void DispalyStopsOptions(ConsoleColor color)
        {
            Console.Clear();
            ConsoleUtils.DisplayHeader("BUS STATION SYSTEM", color);
            Console.ForegroundColor = color;

            ConsoleUtils.DrawMenuTable("HR Operations", new string[] {
                "1. Add Stop",
                "2. Show Stops",
                "3. Update Stop",
                "4. Delete Stop",
                "5. Go Back"
            });

            Console.ResetColor();
        }
        static void DispalyEmployeeOptions(ConsoleColor color)
        {
            Console.Clear();
            ConsoleUtils.DisplayHeader("BUS STATION SYSTEM", color);
            Console.ForegroundColor = color;

            ConsoleUtils.DrawMenuTable("HR Operations", new string[] {
                "1. Add Employee",
                "2. Show All Employees",
                "3. Update Employee Stop",
                "4. Delete Employee",
                "5. Go Back"
            });

            Console.ResetColor();
        }
        static void DispalyDriverOptions(ConsoleColor color)
        {
            Console.Clear();
            ConsoleUtils.DisplayHeader("BUS STATION SYSTEM", color);
            Console.ForegroundColor = color;

            ConsoleUtils.DrawMenuTable("HR Operations", new string[] {
                "1. Add Driver",
                "2. Show All Drivers",
                "3. Update Driver Bus",
                "4. Delete Driver",
                "5. Go Back"
            });

            Console.ResetColor();
        }

        // HR menu
        public static void HandleHrOptions(int mainOption)
        {
            switch ((HROptions)mainOption)
            {
                case HROptions.Stops:
                    {
                        DispalyStopsOptions(ConsoleColor.DarkGray);
                        
                        HandleStopsOptions();

                    }
                    break;

                case HROptions.Employees:
                    {
                        DispalyEmployeeOptions(ConsoleColor.DarkGray);
                        int userInput = ValidationHelper.GetValidUserChoice(1, 5);
                        HandleEmployeeOptions(userInput);
                    }
                    break;

                case HROptions.Drivers:
                    {
                        DispalyDriverOptions(ConsoleColor.DarkGray);
                        int userInput = ValidationHelper.GetValidUserChoice(1, 5);
                        HandleDriverOptions(userInput);
                    }
                    break;

                case HROptions.Back:
                    Program.Main(null);
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

        }
        //stops menu
        static void HandleStopsOptions()
        {

            if (int.TryParse(Console.ReadLine(), out int stopsOption))
            {
                switch ((Options)stopsOption)
                {
                    case Options.Add:
                        {

                            StopsRepository.AddStop();
                            BackToHrMenu();
                        }

                        break;

                    case Options.Show:
                        {
                            StopsRepository.ShowStops();
                            BackToHrMenu();
                        }

                        break;

                    case Options.Update:
                        {
                            StopsRepository.ModifyStop();
                            BackToHrMenu();
                        }
                        break;

                    case Options.Delete:
                        {
                            StopsRepository.RemoveStop();
                            BackToHrMenu();
                        }
                        break;
                    case Options.Back:
                        {
                            DisplayHROptions(ConsoleColor.DarkGray);
                            int userInput = ValidationHelper.GetValidUserChoice(1, 4);
                            HandleHrOptions(userInput);
                        }
                        break;

                    default:
                        ConsoleUtils.DisplayErrorMessage("Invalid Input");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static void HandleEmployeeOptions(int option)
        {
            switch ((Options)option)
            {
                case Options.Add:
                    {

                        EmployeesRepository.AddEmployee();
                        BackToHrMenu();
                    }

                    break;

                case Options.Show:
                    {
                        EmployeesRepository.ShowAllEmployees();
                        BackToHrMenu();
                    }

                    break;

                case Options.Update:
                    {
                        EmployeesRepository.UpdateEmployeeStopId();
                        BackToHrMenu();
                    }
                    break;

                case Options.Delete:
                    {
                        EmployeesRepository.DeleteEmployee();
                        BackToHrMenu();
                    }
                    break;

                case Options.Back:
                    {
                        DisplayHROptions(ConsoleColor.DarkGray);
                        int userInput = ValidationHelper.GetValidUserChoice(1, 4);
                        HandleHrOptions(userInput);
                    }
                    break;

                default:
                    ConsoleUtils.DisplayErrorMessage("Invalid Input");
                    break;
            }

        }

        static void HandleDriverOptions(int option)
        {
            switch ((Options)option)
            {
                case Options.Add:
                    {

                        DriversRepository.AddDriver();
                        BackToHrMenu();

                    }

                    break;

                case Options.Show:
                    {
                        DriversRepository.ShowAllDrivers();
                        BackToHrMenu();
                    }

                    break;

                case Options.Update:
                    {
                        DriversRepository.UpdateDriverBusId();
                        BackToHrMenu();
                    }
                    break;

                case Options.Delete:
                    {
                        DriversRepository.DeleteDriver();
                        BackToHrMenu();
                    }
                    break;

                case Options.Back:
                    {
                        DisplayHROptions(ConsoleColor.DarkGray);
                        int userInput = ValidationHelper.GetValidUserChoice(1, 4);
                        HandleHrOptions(userInput);
                    }
                    break;

                default:
                    ConsoleUtils.DisplayErrorMessage("Invalid Input");
                    break;
            }


        }

        static void BackToHrMenu()
        {
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
            HandleDriverOptions(5);
        }



    }
}
