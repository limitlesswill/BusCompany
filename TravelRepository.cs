using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_station
{
    static class TravelRepository
    {
        private static Travel[] travelArray = new Travel[50];
        private static int numOfTravel = 0;

        //public static Travel GetTravelDetailsFromUser()
        //{
        //    Console.Write("Enter No_seat:");
        //    string No_seat = Console.ReadLine();

        //    Console.Write("Enter Date:");
        //    TimeSpan Date = TimeSpan.Parse(Console.ReadLine());

        //    Console.Write("Enter Id_passenger:");
        //    int Id_passenger = int.Parse(Console.ReadLine());

        //    Console.Write("Enter Id_bus:");
        //    int Id_bus = int.Parse(Console.ReadLine());

        //    Console.Write("Enter Id_trip:");
        //    int Id_trip = int.Parse(Console.ReadLine());

        //    Console.Write("Enter Is_deleted:");
        //    bool Is_deleted = bool.Parse(Console.ReadLine());

        //    return new Travel
        //    {
        //        No_seat = No_seat,
        //        Date = Date,
        //        Id_passenger = Id_passenger,
        //        Id_bus = Id_bus,
        //        Id_trip = Id_trip,
        //        Is_deleted = Is_deleted
        //    };
        //}
        public static void drawBusSeats()
        {

            Console.Write(" ----  ");
            Console.Write(" ----  ");
                Console.Write(" ----  ");
                Console.Write(" ----  ");
                Console.Write(" ----  \n");
            Console.Write(" |1f | ");
            Console.Write(" |2f | ");
            Console.Write(" |3f | ");
            Console.Write(" |4f | ");
            Console.Write(" |5f |\n");
            Console.Write(" ----  ");
            Console.Write(" ----  ");
            Console.Write(" ----  ");
            Console.Write(" ----  ");
            Console.Write(" ----  \n");
             for(int i = 0; i <= 11; i++) {
                if (i == 4)
                {
                    Console.Write(" ----  ");
                    Console.Write(" ----  ");
                    Console.Write("       ");
                    Console.Write("       ");
                    Console.Write("       \n");
                    Console.Write($" |{i}a | ");
                    Console.Write($" |{i}b | ");
                    Console.Write("       ");
                    Console.Write($"      ");
                    Console.Write($"      \n");
                    Console.Write(" ----  ");
                    Console.Write(" ----  ");
                    Console.Write("       ");
                    Console.Write("       ");
                    Console.Write("       \n");
                }else if (i >= 10)
                {
                    Console.Write(" ----  ");
                    Console.Write(" ----  ");
                    Console.Write("       ");
                    Console.Write(" ----  ");
                    Console.Write(" ----  \n");
                    Console.Write($" |{i}a| ");
                    Console.Write($" |{i}b| ");
                    Console.Write("       ");
                    Console.Write($" |{i}c| ");
                    Console.Write($" |{i}d|\n");
                    Console.Write(" ----  ");
                    Console.Write(" ----  ");
                    Console.Write("       ");
                    Console.Write(" ----  ");
                    Console.Write(" ----  \n");
                }
                else
                {
                    Console.Write(" ----  ");
                    Console.Write(" ----  ");
                    Console.Write("       ");
                    Console.Write(" ----  ");
                    Console.Write(" ----  \n");
                    Console.Write($" |{i}a | ");
                    Console.Write($" |{i}b | ");
                    Console.Write("       ");
                    Console.Write($" |{i}c | ");
                    Console.Write($" |{i}d |\n");
                    Console.Write(" ----  ");
                    Console.Write(" ----  ");
                    Console.Write("       ");
                    Console.Write(" ----  ");
                    Console.Write(" ----  \n");
                }

            }


        }

        public static void drawBusSeats(DateTime tripdate, int tripId, int busId)
        {
            string[] lastRaw = new string[5] {" 1f", " 2f", " 3f", " 4f", " 5f"};
            drawRawOfSeats(lastRaw,tripdate,tripId, busId); 
            for (int i = 1; i <= 11; i++)
            {
                string[] raw;
                if (i > 9)
                {
                    raw=new string[5] {$"{i}a", $"{i}b", "NoSeat", $"{i}c", $"{i}d"};
                }else if (i == 4)
                {
                    raw= new string[5] { $"{i}a", $"{i}b", "NoSeat", "NoSeat", "NoSeat" };
                }
                else
                {
                    raw = new string[5] { $" {i}a", $" {i}b", "NoSeat", $" {i}c", $" {i}d" };
                }

                    drawRawOfSeats(raw, tripdate, tripId, busId);
            }
            Console.ResetColor();
        }
        static void drawRawOfSeats(string[]seats, DateTime tripdate, int tripId, int busId)
        {
            Dictionary<string, placeStatus> statuses = checkRawOfSeatsStatus(seats, tripdate, tripId, busId);
            foreach(string seat in seats)
            {
                if (statuses[seat]==placeStatus.noSeat) {
                    Console.Write("       ");
                }
                else
                {
                    if (statuses[seat]==placeStatus.free)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if(statuses[seat]==placeStatus.booked)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write(" ----  ");
                }
            }
            Console.Write("\n");
            foreach (string seat in seats)
            {
                if (statuses[seat] == placeStatus.noSeat)
                {
                    Console.Write("       ");
                }
                else
                {
                    if (statuses[seat] == placeStatus.free)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (statuses[seat] == placeStatus.booked)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($" |{seat}| ");
                }
            }
            Console.Write("\n");
            foreach (string seat in seats)
            {
                if (statuses[seat] == placeStatus.noSeat)
                {
                    Console.Write("       ");
                }
                else
                {
                    if (statuses[seat] == placeStatus.free)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (statuses[seat] == placeStatus.booked)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write(" ----  ");
                }
            }
            Console.Write("\n");
        } 
        enum placeStatus
        {
            free=1,
            booked,
            noSeat
        }
        static Dictionary<string,placeStatus> checkRawOfSeatsStatus(string[]seats, DateTime tripdate, int tripId, int busId)
        {
            Dictionary<string, placeStatus> resault=new Dictionary<string, placeStatus>();
            resault["NoSeat"] = placeStatus.noSeat;
            foreach (string seat in seats)
            {
                if(seat!= "NoSeat")
                {
                  if(DatabaseHandler.checkIfTheSeatIsToken(seat, tripdate, tripId, busId))
                    {
                        resault.Add(seat, placeStatus.booked);
                    }
                    else
                    {
                        resault.Add(seat, placeStatus.free);
                    }
                }

            }
            return resault;
        }
        public static void AddTravel(Travel Travel)
        {
            DatabaseHandler.InsertTravel(Travel);
        }
        public static int getTheTicketNeedToDelete(int passengerId,DateTime date)
        {
            DatabaseHandler.getAllTicketsForThisClientInThatDay(passengerId, date,out travelArray,ref numOfTravel);
           if(numOfTravel > 0)
            {
                for (int i = 0; i < numOfTravel; i++)
                {
                    Console.WriteLine($"{i + 1}- seat {travelArray[i].No_seat} in bus {travelArray[i].Id_bus}");
                }
                return travelArray[ValidationHelper.GetValidUserChoice(1, numOfTravel) - 1].Id_travel;
            }
            else
            {
                return 0;
            }

        }
        public static void deleteThisReservation(int travelId) {

            DatabaseHandler.cancelTravel(travelId);
        }
    }
}
