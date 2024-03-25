


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_station
{
    static class StopsRepository
    {
        private static Stops[] StopsArray = new Stops[50];
        private static int numOfStops = 0;
        public static int NumOfStops { get=>numOfStops;  }
        public static Stops[] stops { get=>StopsArray;  }

        public static Stops GetStopDetailsFromUser()
        {
            Console.Write("Enter ID:");
            int ID = int.Parse(Console.ReadLine());
            Console.Write("Enter City name:");
            string cityName = Console.ReadLine();

            Console.Write("Enter address:");
            string address = Console.ReadLine();




            return new Stops
            {
                Id = ID,
                City = cityName,
                Address = address,


            };
        }
        //add stop
        public static void AddStop()
        {
            Stops stop = GetStopDetailsFromUser();
            DatabaseHandler.InsertStop(stop);
           
        }

        public static void ShowStops()
        {
            DatabaseHandler.getAllStops(out StopsArray,ref numOfStops);

            if (numOfStops > 0)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("| ID |        City       |       Address       | Is Deleted |");
                Console.WriteLine("---------------------------------------------------------------");

                for (int i =0; i < numOfStops;i ++)
                {
                    Console.WriteLine($"| {StopsArray[i].Id,-3} | {StopsArray[i].City,-18} | {StopsArray[i].Address,-21} | {StopsArray[i].Is_deleted,-11} |");
                }

                Console.WriteLine("---------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("No stops found.");
            }
            
        }
        public static void ShowAvailableStops()
        {
            DatabaseHandler.getAllStops(out StopsArray, ref numOfStops);

            if (numOfStops > 0)
            {
                for(int i = 0; i < numOfStops; i++)
                {
                    Console.WriteLine($"{i + 1}-{StopsArray[i]}");
                }
            }
            else
            {
                Console.WriteLine("No stops found.");
            }
        }
        public static void getallTripsEndsThatStartFrom(int startid)
        {
            DatabaseHandler.getallTripsEndsThatStartFrom(startid,out StopsArray, ref numOfStops);
            if (numOfStops > 0)
            {
                for (int i = 0; i < numOfStops; i++)
                {
                    Console.WriteLine($"{i + 1}-{StopsArray[i]}");
                }
            }
            else
            {
                Console.WriteLine("No stops found.");
            }
        }
        //modify stop
        public static void ModifyStop()
        {
            Console.WriteLine("enter the id of the stop that you want to update:");

            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("enter the data of new stop:");

            Stops stop = GetStopDetailsFromUser();

            DatabaseHandler.UpdateStop(id, stop);
            
        }
        //delete stop
        public static void RemoveStop()
        {
            Console.WriteLine("enter the id of the stop that you want to delete:");

            int id = int.Parse(Console.ReadLine());


            DatabaseHandler.DeleteStop(id);
           
        }


    }
}


