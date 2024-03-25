using System;
using System.Collections.Generic;


namespace bus_station
{
    static class BusRepository
    {
        private static Bus[] busArray = new Bus[50];
        private static int numOfBus = 0;
        public static Bus[] buses { get => busArray; }
        public static int NumOfBus { get => numOfBus; }
        
        public static Bus GetBusDetailsFromUser()
        {
            /*
		private int id_bus;
        private int Number;
        private string Class;
        private int no_seat;
			*/

            Console.Write("Enter Bus ID:");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Bus number:");
            int Number = int.Parse(Console.ReadLine());

            Console.Write("Enter bus class:");
            string Class = Console.ReadLine();

            Console.Write("Enter bus seat number:");
            int no_seat = int.Parse(Console.ReadLine());

            return new Bus
            {
                id_bus = id,
                Number = Number,
                Class = Class,
                no_seat = no_seat
            };
        }

        public static void AddBus()
        {
            Bus bus = GetBusDetailsFromUser();
            DatabaseHandler.InsertBus(bus);
        }
        public static void DeleteBus()
        {
            Console.WriteLine("Enter bus ID:");
            int busID = int.Parse(Console.ReadLine());
            DatabaseHandler.DeleteBus(busID);
        }

        public static void getBusesOnThisTripThatDay(DateTime date, int trip)
        {
            List<int> numOfTokenSeats = new List<int>();
            DatabaseHandler.getBusesOnThisTripThatDay(trip, date,out busArray, ref numOfBus, numOfTokenSeats);
            Bus[] buses = new Bus[50];
            int numOfBusesWithAvailblePlace = 0;
            for (int i = 0; i < numOfBus; i++)
            {
                if (busArray[i].no_seat > numOfTokenSeats[i])
                {
                    buses[numOfBusesWithAvailblePlace++] = busArray[i]; 
                }
            }
            busArray = buses;
            numOfBus = numOfBusesWithAvailblePlace;
            for (int i = 0; i < numOfBus; i++)
            {
                if (busArray[i].no_seat > numOfTokenSeats[i])
                {
                    Console.WriteLine($"{i + 1}-{busArray[i]}");
                }
            }
        }

        public static void getFreeBusesInThatDay(DateTime date)
        {
            DatabaseHandler.getFreeBusesInThatDay(date, out busArray, ref numOfBus);
            for (int i = 0; i < numOfBus; i++)
            {
                Console.WriteLine($"{i + 1}-{busArray[i]}");
            }
        }

        public static void ShowAllBuses()
        {
            DatabaseHandler.GetAllBuses(busArray, ref numOfBus);

            string[] buses = new string[numOfBus];
            for (int i = 0; i < numOfBus; i++)
            {
                if (!busArray[i].isDeleted)
                {
                    buses[i] = ($"{i + 1} - {busArray[i]}");
                }
            }

            ConsoleUtils.DrawMenuTable("Buses:", buses);
        }

    }

}