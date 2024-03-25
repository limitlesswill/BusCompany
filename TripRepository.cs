using System;
using System.Collections.Generic;


namespace bus_station
{
    static class TripRepository
    {
        private static Trip[] tripArray = new Trip[50];
        private static int numOftrips = 0;
        public static Trip[] Trips { get => tripArray; }
        public static int numOfTrips { get { return numOftrips; } }
        public static Trip GetTripsDetailsFromUser()
        {
            /*
        public int Id_Trip { get; set; }
        public string Time { get; set; }
        public float Price { get; set; }
        public int id_start { get; set; }
        public int id_end  { get; set; }
			*/

            Console.Write("Enter Trip ID:");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Trip time:");
            TimeSpan time = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter Trip price:");
            float price = float.Parse(Console.ReadLine());

            Console.Write("Enter id_start:");
            int id_start = int.Parse(Console.ReadLine());

            Console.Write("Enter id_end:");
            int id_end = int.Parse(Console.ReadLine());

            return new Trip
            {
                Id_Trip = id,
                Time = time,
                Price = price,
                id_start = id_start,
                id_end = id_end
            };
        }

        public static void AddTrips()
        {

            Trip trip = GetTripsDetailsFromUser();
            DatabaseHandler.InsertTrip(trip);
        }
        public static void DeleteTrip()
        {
            Console.Write("Enter Trip id: ");
            int id = int.Parse(Console.ReadLine());
            DatabaseHandler.DeleteTrip(id);
        }
        public static void getAllTripsGoInThisLine(int start, int end)
        {
            DatabaseHandler.getAllTripsGoInThisLine(start, end,out tripArray, ref numOftrips);
            Console.WriteLine("here is our daily trips for  this line please choose the one that is suitable to you");
            for (int i = 0; i < numOftrips; i++)
            {
                Console.WriteLine($"{i + 1}-{tripArray[i].Time}");
            }
        }

    }
}