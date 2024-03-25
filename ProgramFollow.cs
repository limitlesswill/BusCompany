using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bus_station
{
    static class ProgramFollow
    {
        public static void DisplayMainMenu(ConsoleColor color)
        {
            Console.Clear();
            ConsoleUtils.DisplayHeader("BUS STATION SYSTEM", color);
            Console.ForegroundColor = color;

            ConsoleUtils.DrawMenuTable("Choose a module", new string[] {
                "1. Ticket Reservation",
                "2. HR Operations",
                "3. Management",
                "4. Exit"
            });

            Console.ResetColor();

        }

       

        public static void HandleUserChoice(int userChoice)
        {
            Module selectedModule = (Module)userChoice;

            switch (selectedModule)
            {
                case Module.ClientPart:
                    {
                        DisplayClientMenu(ConsoleColor.DarkGray);

                        int userInput = ValidationHelper.GetValidUserChoice(1, 4);
                        HandleClientPart(userInput);
                    }
                    break;

                case Module.HROperations:
                    {
                        HrMenu.DisplayHROptions(ConsoleColor.DarkGray);
                        int userInput = ValidationHelper.GetValidUserChoice(1, 4);
                        HrMenu.HandleHrOptions(userInput);
                    }
                    break;

                case Module.Management:
                    {
                    }
                    break;

                case Module.Exit:
                    {
                        return;
                    }
                default:
                    {
                        ConsoleUtils.DisplayErrorMessage("Invalid choice selected." +
                            "Please select one of the below choices.");
                        DisplayMainMenu(ConsoleColor.DarkCyan);
                    }
                    break;
            }

        }

        static void DisplayClientMenu(ConsoleColor color)
        {
            Console.Clear();
            ConsoleUtils.DisplayHeader("BUS STATION SYSTEM", ConsoleColor.DarkCyan);
            Console.ForegroundColor = color;

            ConsoleUtils.DrawTableHeader("Ticket Reservation", 32);

            ConsoleUtils.DrawMenuTable("What does the client need to do:", new string[] {
                "1. Registration",
                "2. Book a Ticket",
                "3. Cancel a Ticket",
                "4. Go Back"
            });

            Console.ResetColor();
            int userInput = ValidationHelper.GetValidUserChoice(1, 4);
            HandleClientPart(userInput);

        }
        static void HandleClientPart(int userChoice)
        {
            ClientEnum selectedModule = (ClientEnum)userChoice;

            switch (selectedModule)
            {
                case ClientEnum.addPassenger:
                    {
                        PassengerController.addNewPassenger();
                        Console.ReadKey();
                        DisplayClientMenu(ConsoleColor.DarkGray);
                    }
                    break;

                case ClientEnum.bookTicket:
                    {
                        int passengerId = PassengerController.getPassengerWithPhoneNum();
                        if (passengerId != 0)
                        {
                            int startId = chooseStartPoint();
                            int endId = chooseStopPoint(startId);
                            DateTime tripDate = enterDateOfTrip();
                            int tripId = chooseTheTrip(startId, endId);
                            int busId = chooseTheSuitableBus(tripDate, tripId);
                            if (busId != 0)
                            {
                                string seatNum = chooseSeatNum(tripId, busId, tripDate);
                                Travel ticket = new Travel();
                                ticket.No_seat = seatNum;
                                ticket.Date = tripDate;
                                ticket.Id_passenger = passengerId;
                                ticket.Id_trip = tripId;
                                ticket.Id_bus = busId;
                                TravelRepository.AddTravel(ticket);
                                Console.WriteLine("press any key to continue");
                                Console.ReadKey();
                                DisplayClientMenu(ConsoleColor.DarkGray);
                            }
                            else
                            {
                                Console.WriteLine("press any key to continue");
                                Console.ReadKey();
                                DisplayClientMenu(ConsoleColor.DarkGray);
                            }
                        }
                        else
                        {
                            Console.WriteLine("press any key to continue");
                            Console.ReadKey();
                            DisplayClientMenu(ConsoleColor.DarkGray);
                        }
                    }
                    break;

                case ClientEnum.ticketCancel:
                    {
                        int passengerId = PassengerController.getPassengerWithPhoneNum();
                        DateTime tripDate = enterDateOfTrip();
                        int deletedTicket = TravelRepository.getTheTicketNeedToDelete(passengerId, tripDate);
                        if (deletedTicket != 0) {
                            TravelRepository.deleteThisReservation(deletedTicket);
                        }
                        else
                        {
                            Console.WriteLine("this client have no tickets in this day");
                        }
                        Console.ReadKey();
                        DisplayClientMenu(ConsoleColor.DarkGray);
                    }
                    break;

                case ClientEnum.back:
                    {
                        //DisplayMainMenu(ConsoleColor.DarkCyan);
                        Program.Main(null);
                    }
                    break;

                default:
                    {
                        ConsoleUtils.DisplayErrorMessage("Invalid choice selected." +
                            "Please select one of the below choices.");

                        DisplayClientMenu(ConsoleColor.DarkGray);
                    }
                    break;
            }

        }
        static int chooseStartPoint()
        {
            Console.WriteLine("choose where your trip need to start");
            StopsRepository.ShowAvailableStops();
            int StartStopIndex=ValidationHelper.GetValidUserChoice(1, StopsRepository.NumOfStops);
            return StopsRepository.stops[StartStopIndex - 1].Id;
        }
        static int chooseStopPoint(int startId)
        {
            StopsRepository.getallTripsEndsThatStartFrom( startId);
            Console.WriteLine("choose where your trip need to go");
            int endStopIndex=ValidationHelper.GetValidUserChoice(1, StopsRepository.NumOfStops);
           return StopsRepository.stops[endStopIndex - 1].Id;
        }
        static DateTime enterDateOfTrip()
        {
            DateTime tripdate;
            do
            {
                bool valid = true;
                int month;
                do
                {
                    Console.Write("Enter a month: ");
                    valid = int.TryParse(Console.ReadLine(), out month);
                    if (valid && (month > 12 || month < 1))
                    {
                        valid = false;
                    }
                } while (!valid);
                valid = true;
                int day;
                do
                {
                    Console.Write("Enter a day: ");
                    valid = int.TryParse(Console.ReadLine(), out day);
                    int limit;
                    if (month == 2)
                    {
                        limit = 29;
                    }
                    else if (month == 4 || month == 6 || month == 9 || month == 11)
                    {
                        limit = 30;
                    }
                    else
                    {
                        limit = 31;
                    }
                    if (valid && (day > limit || day < 1))
                    {
                        valid = false;
                    }
                } while (!valid);
                int year;
                do
                {
                    Console.Write("Enter a year: ");
                    valid = int.TryParse(Console.ReadLine(), out year);
                    if (valid && (year < DateTime.Now.Year || year > DateTime.Now.Year + 1))
                    {
                        valid = false;
                    }
                } while (!valid);
                tripdate = new DateTime(year, month, day);
            } while (tripdate < DateTime.Today);
            return tripdate;
        }
        static int chooseTheTrip(int startId,int endId)
        {
            TripRepository.getAllTripsGoInThisLine(startId, endId);
            Console.WriteLine("choose one of these appointments");
            int ourChoosenTripIndex=ValidationHelper.GetValidUserChoice(1, TripRepository.numOfTrips);
            return TripRepository.Trips[ourChoosenTripIndex - 1].Id_Trip;
        }
        static int chooseTheSuitableBus(DateTime tripDate,int tripId)
        {
            BusRepository.getBusesOnThisTripThatDay(tripDate, tripId);
            if (BusRepository.NumOfBus == 0)
            {
                BusRepository.getFreeBusesInThatDay(tripDate);
            }
            if (BusRepository.NumOfBus != 0)
            {
                Console.WriteLine("choose the suitable bus for this ticket");
                int ourChoosenBusIndex=ValidationHelper.GetValidUserChoice(1, BusRepository.NumOfBus);
                return BusRepository.buses[ourChoosenBusIndex - 1].id_bus;
            }
            else
            {
                ConsoleUtils.DisplayErrorMessage("unfortunatlely we have no available seats onthis trip that day and have no free buses to increase our fleet for this trip that day");
                return 0;
            }
        }
        static string chooseSeatNum(int tripId, int busId, DateTime tripDate)
        {
            Console.WriteLine("please choose your seat");
            TravelRepository.drawBusSeats(tripDate, tripId, busId);
            string seatNum;
            do
            {
                seatNum = Console.ReadLine();
            } while (!(Regex.Match(seatNum, "^([1-9]|10|11)[a-b]{1}$").Success|| Regex.Match(seatNum, "^([1-3]|[5-9]|10|11)[c-d]{1}$").Success|| Regex.Match(seatNum, "^[1-5](f){1}$").Success));
            return seatNum;
        }
    }
}
