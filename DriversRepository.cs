using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_station
{
    static class DriversRepository
    {
        private static Driver[] driversArray = new Driver[50];
        private static int numOfDrivers = 0;

        private static Driver GetDriverDetailsFromUser()
        {
            Console.Write("Enter id:");
            int id =  ValidationHelper.GetValidIntegerInput();

            Console.Write("Enter name:");
            string name = ValidationHelper.GetNonEmptyStringInput();

            Console.Write("Enter address:");
            string address = ValidationHelper.GetNonEmptyStringInput();

            int stopId = SelectBusId();

            return new Driver
            {
                Id = id,
                Name = name,
                Address = address,
                BusId = stopId,
                IsDeleted = false
            };
        }

        public static void AddDriver()
        {
            Driver driver = GetDriverDetailsFromUser();
            DatabaseHandler.InsertDriver(driver);
        }

        public static void ShowAllDrivers()
        {
            DatabaseHandler.GetAllDrivers(driversArray, ref numOfDrivers);

            string [] drivers = new string[numOfDrivers];
            for (int i = 0; i < numOfDrivers; i++)
            {
                if (!driversArray[i].IsDeleted)
                {
                    drivers[i] = ($"{i + 1} - {driversArray[i]}");
                }
            }

            ConsoleUtils.DrawMenuTable("Drivers:", drivers);
        }

        public static void UpdateDriverBusId()
        {
            int id = SelectDrivereId();

            int bus_id = SelectBusId();

            DatabaseHandler.UpdateDriverBusId(id, bus_id);
        }

        public static void DeleteDriver()
        {
            int id = SelectDrivereId();
            DatabaseHandler.DeleteDriver(id);
        }

        static int SelectBusId()
        {
            Console.WriteLine("\nchoose one of the buses:");
            Console.WriteLine("\nAvailable buses:");
            BusRepository.ShowAllBuses();

            int busIndex = ValidationHelper.GetValidUserChoice(1, BusRepository.NumOfBus);

            int busId = BusRepository.buses[busIndex - 1].id_bus;

            return busId;
        }

        private static int SelectDrivereId()
        {
            Console.WriteLine("All Drivers:");
            ShowAllDrivers();

            Console.Write("Enter the ID of the driver you would like to select: ");

            int driverIndex = ValidationHelper.GetValidUserChoice(1, numOfDrivers);

            int driverId = driversArray[driverIndex - 1].Id;

            return driverId;
        }

    }
}
