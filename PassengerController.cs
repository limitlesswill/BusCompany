using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bus_station
{
    static class PassengerController
    {
        static Passenger[] passengers = new Passenger[50];
        static int lastPassengerWeGet = 0;
        public static void getPassengerWithName()
        {
            Console.WriteLine("please enter the passenger name");
            string passengerName = Console.ReadLine();
            passengers = new Passenger[50];
            lastPassengerWeGet = 0;
            DatabaseHandler.GetAllPassengersThatMatchName(passengerName, passengers, ref lastPassengerWeGet);
            if (lastPassengerWeGet > 0)
            {
                Console.WriteLine("result of the search");
               for(int i = 0; i < lastPassengerWeGet; i++)
                {
                    Console.WriteLine($"{i+1}-{passengers[i]}");
                }
            }else { Console.WriteLine("there is no passengers with such name"); }

        }
        public static int getPassengerWithPhoneNum()
        {
            string passengerNum;
            do
            {
                Console.WriteLine("please enter the passenger phone number correctly");
                passengerNum = Console.ReadLine();

            } while (!Regex.Match(passengerNum, @"01[0-9]{9}$").Success);
            Passenger passenger = new Passenger("", "");
            DatabaseHandler.GetPassengerThatMatchPhoneNumber(passengerNum, out passenger);
            if (passenger.ID>0)
            {
                return passenger.ID;
            }
            else { Console.WriteLine("this number never be recorded before please add him for our system");return 0; }

        }
        public static void addNewPassenger()
        {
            Console.WriteLine("enter the passenger name");
            string passengerName = ValidationHelper.getNameInput();
            string phoneNum;
            do
            {
                Console.WriteLine("enter the passenger phone number");
                phoneNum = Console.ReadLine();

            } while (!Regex.Match(phoneNum, @"01[0-9]{9}$").Success);
            Passenger passenger = new Passenger(passengerName, phoneNum);
            DatabaseHandler.InsertPassengers(passenger);
        }
    }
}
