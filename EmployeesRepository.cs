using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_station
{
    class EmployeesRepository
    {
        private static Employee[] employeesArray = new Employee[50];
        private static int numOfEmployees = 0;

        private static Employee GetEmployeeDetailsFromUser()
        {
            Console.Write("Enter id:");
            int id = ValidationHelper.GetValidIntegerInput();

            Console.Write("Enter name:");
            string name = ValidationHelper.GetNonEmptyStringInput();

            Console.Write("Enter salary:");
            double salary = ValidationHelper.GetValidDoubleInput();

            int stopId = SelectStopId();

            return new Employee
            {
                Id = id,
                Name = name,
                Salary = salary,
                StopId = stopId,
                IsDeleted = false
            };
        }
 
        public static void AddEmployee()
        {
            Employee employee = GetEmployeeDetailsFromUser();
            DatabaseHandler.InsertEmployee(employee);
        }

        public static void ShowAllEmployees()
        {
            DatabaseHandler.GetAllEmployees(employeesArray, ref numOfEmployees);

            string[] employees = new string[numOfEmployees];
            for (int i = 0; i < numOfEmployees; i++)
            {
                if (!employeesArray[i].IsDeleted)
                {
                    employees[i] = ($"{i + 1} - {employeesArray[i]}");
                }
            }

            ConsoleUtils.DrawMenuTable("Employees:", employees);
        }

        public static void UpdateEmployeeStopId()
        {
            int id = SelectEmployeeId();

            int stopId = SelectStopId();

            DatabaseHandler.UpdateEmployeeStopId(id, stopId);
        }

        public static void DeleteEmployee()
        {
            int id = SelectEmployeeId();
            DatabaseHandler.DeleteEmployee(id);
        }

        static int SelectStopId()
        {
            Console.WriteLine("\nchoose one of the stops:");
            Console.WriteLine("\nAvailable Stops:");
            StopsRepository.ShowStops(); 

            int stopIndex = ValidationHelper.GetValidUserChoice(1, StopsRepository.NumOfStops);

            int stopId = StopsRepository.stops[stopIndex - 1].Id;

            return stopId;
        }

        private static int SelectEmployeeId()
        {
            Console.WriteLine("All Employees:");
            ShowAllEmployees();

            Console.Write("Enter the ID of the employee you would like to select: ");

            int empIndex = ValidationHelper.GetValidUserChoice(1, numOfEmployees);

            int empId = employeesArray[empIndex - 1].Id;

            return empId;
        }

    }
}
