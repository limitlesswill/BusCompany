using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace bus_station
{
    static class DatabaseHandler
    {
        private static string connectionString = "Data Source =.;" +
           "Initial Catalog=Bus; Integrated Security=True";
        // - improvement : It's recommended to store the connection string in a configuration file
        // instead of hardcoding it in your code. This makes it easier to manage and update without modifying the code

        private static SqlConnection GetOpenConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        private static int ExecuteNonQuery(string query, params (string, object)[] parameters)
        {
            try
            {
                using (SqlConnection con = GetOpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, con);

                    foreach (var (paramName, paramValue) in parameters)
                    {
                        cmd.Parameters.AddWithValue(paramName, paramValue);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 34) == "Violation of UNIQUE KEY constraint")
                {
                    Console.WriteLine("this record is added before please try not to repeat the data that must be unique for this record");
                }
                else
                {
                    ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");  
                }
                return -1;
            }
        }

        private static SqlDataReader ExecuteQuery(string query)
        {
            try
            {
                SqlConnection con = GetOpenConnection();
                SqlCommand cmd = new SqlCommand(query, con);

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
                return null;
            }
        }
        private static object ExecuteScalarQuery(string query)
        {
            try
            {
                using (SqlConnection con = GetOpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, con);



                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                //if (ex.Message.Substring(0, 34) == "Violation of UNIQUE KEY constraint")
                //{
                //    Console.WriteLine("this record is added before please try not to repeat the data that must be unique for this record");
                //}
                //else
                //{
                    ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
                //}
                return -1;
            }
        }

        //driver
        public static void InsertDriver(Driver driver)
        {
            string query = "INSERT INTO driver (Id_driver, Name, Address, id_bus, is_deleted) VALUES (@id, @name, @address, @busId, @isDeleted)";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    ("@id", driver.Id),
                    ("@name", driver.Name),
                    ("@address", driver.Address),
                    ("@busId", driver.BusId),
                    ("@isDeleted", driver.IsDeleted)
                );

                ConsoleUtils.DisplaySuccessMessage($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
            }
        }
        public static void GetAllDrivers(Driver[] drivers, ref int numOfDrivers)
        {
            try
            {
                string query = "SELECT * FROM driver";

                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            if (!(bool)reader[4])// if not deleted
                            {
                                Driver driver = new Driver();
                                driver.Id = (int)reader[0];
                                driver.Name = (string)reader[1];
                                driver.Address = (string)reader[2];
                                driver.BusId = (int)reader[3];
                                driver.IsDeleted = (bool)reader[4];

                                drivers[counter++] = driver;
                            }
                        }
                        numOfDrivers = counter;
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
            }
        }
        public static void UpdateDriverBusId(int id, int newBusId)
        {
            try
            {
                string query = "UPDATE driver SET id_bus = @newBusId WHERE id_driver = @id";

                int rowsAffected = ExecuteNonQuery(query,
                    ("@newBusId", newBusId),
                    ("@id", id)
                );

                ConsoleUtils.DisplaySuccessMessage($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
            }
        }
        public static void DeleteDriver(int id)
        {
            try
            {
                string query = "UPDATE driver SET is_deleted = @isDeleted WHERE id_driver = @id";

                int rowsAffected = ExecuteNonQuery(query,
                    ("@isDeleted", 1),
                    ("@id", id)
                );

                ConsoleUtils.DisplaySuccessMessage($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
            }
        }

        //employee
        public static void InsertEmployee(Employee employee)
        {
            string query = "INSERT INTO employee (Id_employee, Name, Salary, id_stop, is_deleted) VALUES (@id, @name, @salary, @stopId, @isDeleted)";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    ("@id", employee.Id),
                    ("@name", employee.Name),
                    ("@salary", employee.Salary),
                    ("@stopId", employee.StopId),
                    ("@isDeleted", employee.IsDeleted)
                );

                ConsoleUtils.DisplaySuccessMessage($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
            }
        }
        public static void GetAllEmployees(Employee[] employees, ref int numOfEmployees)
        {
            try
            {
                string query = "SELECT * FROM employee";

                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            if (!(bool)reader[4]) // if not deleted
                            {
                                Employee employee = new Employee();
                                employee.Id = (int)reader[0];
                                employee.Name = (string)reader[1];
                                employee.Salary = (double)reader[2];
                                employee.StopId = (int)reader[3];
                                employee.IsDeleted = (bool)reader[4];


                                employees[counter++] = employee;
                            }
                        }
                        numOfEmployees = counter;
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
            }
        }
        public static void UpdateEmployeeStopId(int id, int newStopId)
        {
            try
            {
                string query = "UPDATE employee SET id_stop = @newStopId WHERE Id_employee = @id";

                int rowsAffected = ExecuteNonQuery(query,
                    ("@newStopId", newStopId),
                    ("@id", id)
                );


                ConsoleUtils.DisplaySuccessMessage($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
            }
        }
        public static void DeleteEmployee(int id)
        {
            try
            {
                string query = "UPDATE employee SET is_deleted = @isDeleted WHERE Id_employee = @id";

                int rowsAffected = ExecuteNonQuery(query,
                    ("@isDeleted", 1),
                    ("@id", id)
                );


                ConsoleUtils.DisplaySuccessMessage($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
            }
        }

        //travel
        public static void InsertTravel(Travel Travel)
        {
            string query = "INSERT INTO [dbo].[Travel]      ([no_seat]      ,[Date]      ,[id_passenger]      ,[id_bus]      ,[id_trip])VALUES      (@no_seat,@Date,@id_passenger,@id_bus,@id_trip)";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    // generate id not handelled
                    ("@no_seat", Travel.No_seat),
                    ("@Date", Travel.Date),
                    ("@id_passenger", Travel.Id_passenger),
                    ("@id_bus", Travel.Id_bus),
                    ("@id_trip", Travel.Id_trip)
                );

                if (rowsAffected > 0)
                {
                    ConsoleUtils.DisplaySuccessMessage($"the ticket booked successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Driver Insert Exception: {ex.Message}");
            }
        }
        public static void getAllTicketsForThisClientInThatDay(int passengerId,DateTime date,out Travel[] tikects ,ref int numOfTickets)
        {
            tikects = new Travel[50];
            numOfTickets = 0;
            try
            {
                string query = $"SELECT  [Id_travel] ,[no_seat] ,[Date] ,[id_passenger] ,[id_bus] ,[id_trip] ,[is_deleted]  FROM [Bus].[dbo].[Travel] where ([id_passenger]={passengerId}) and ([Date]='{date:yyyy-MM-dd}')and([is_deleted]=0)";

                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                                Travel ticket = new Travel();
                                ticket.Id_travel= (int)reader[0];
                                ticket.No_seat = (string)reader[1];
                                ticket.Date = (DateTime)reader[2];
                                ticket.Id_passenger = (int)reader[3];
                                ticket.Id_bus = (int)reader[4];
                                ticket.Id_trip = (int)reader[5];
                                ticket.Is_deleted = (bool)reader[6];
                                tikects[numOfTickets++] = ticket;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}        {ex.StackTrace}");
            }

        }
        public static void cancelTravel(int travelId)
        {
            string query = "UPDATE [dbo].[Travel]   SET [is_deleted] = 1 WHERE  [Id_travel] =@travelId";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    ("@travelId", travelId)
                );
                if (rowsAffected > 0)
                {
                    ConsoleUtils.DisplaySuccessMessage($"the ticket canceled successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static bool checkIfTheSeatIsToken(string noSeats,DateTime tripdate,int tripId,int busId)
        {
            try
            {
                string query = $"if Exists(SELECT  Id_travel  FROM  dbo.Travel   WHERE   (no_seat = '{noSeats.Trim()}') AND (Date = '{tripdate.ToString("yyyy-MM-dd")}') AND (id_bus = {busId}) AND (id_trip = {tripId}) AND (is_deleted = 0)) select CAST(1 AS BIT) else select CAST(0 AS BIT)";
                return (bool)ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"checking exception:{ex.Message}");
                return true;
            }
        }


        public static void getAllTripsGoInThisLine(int start, int end,out Trip[] trips, ref int numOfTrips)
        {
            trips = new Trip[50];
            numOfTrips = 0;
            try
            {
                string query = $"SELECT dbo.trip.* FROM dbo.trip WHERE(id_start = {start}) AND(id_end = {end}) AND(is_deleted = 0) ";
                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    numOfTrips = 0;
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Trip trip = new Trip();
                            trip.Id_Trip = (int)reader[0];
                            trip.Time = (TimeSpan)reader[1];
                            trip.Price = (double)reader[2];
                            trip.id_start = (int)reader[3];
                            trip.id_end = (int)reader[4];
                            trip.isDeleted = (bool)reader[5];
                            trips[numOfTrips++] = trip;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message: {ex.StackTrace}, {ex.Message}");
            }
        }
        //passenger
        public static void GetAllPassengersThatMatchName(string name, Passenger[] passengers, ref int numOfPassenWithThisName)
        {
            try
            {
                string query = $"SELECT * FROM [passenger]  where [Name_Passenger]  like '%{name}%'";
                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Passenger passenger = new Passenger((int)reader[0], (string)reader[1], (string)reader[2]);
                            passengers[numOfPassenWithThisName++] = passenger;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message: {ex.Message}");
            }
        }
        public static void GetPassengerThatMatchPhoneNumber(string phone, out Passenger passenger)
        {
            passenger = new Passenger("", "");
            try
            {
                string query = $"SELECT * FROM [passenger]  where [Phone] = '{phone}'";
                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        Console.WriteLine('a');
                        while (reader.Read())
                        {
                            passenger = new Passenger((int)reader[0], (string)reader[1], (string)reader[2]);
                        }
                    }
                    Console.WriteLine(passenger);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message: {ex.Message}");
            }
        }
        public static void InsertPassengers(Passenger passenger)
        {
            string query = "INSERT INTO [passenger] ([Name_Passenger],[Phone]) VALUES ( @name, @phone)";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    ("@name", passenger.Name),
                    ("@phone", passenger.Phone)
                );

                if (rowsAffected > 0)
                {
                    ConsoleUtils.DisplaySuccessMessage("you added your passenger successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Employee Insert Exception: {ex.Message}");
            }
        }
        //buses
        public static void getBusesOnThisTripThatDay(int trip, DateTime date, out Bus[] buses, ref int numOfbuses, List<int> numOfTokenSeats)
        {
            buses = new Bus[50];
            numOfbuses = 0;
            try
            {
                string query = $"SELECT dbo.Travel.id_bus, dbo.bus.Number, dbo.bus.Class, dbo.bus.no_seats, dbo.bus.is_deleted, COUNT(dbo.Travel.Id_travel) AS [number of token Seats] FROM     dbo.Travel INNER JOIN dbo.bus ON dbo.Travel.id_bus = dbo.bus.Id_bus WHERE  (dbo.bus.is_deleted = 0) AND (dbo.Travel.is_deleted = 0) AND (dbo.Travel.Date ='{date.ToString("yyyy-MM-dd")}') AND (dbo.Travel.id_trip = {trip}) GROUP BY dbo.Travel.id_bus, dbo.bus.Number, dbo.bus.Class, dbo.bus.no_seats, dbo.bus.is_deleted";
                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Bus bus = new Bus();
                            bus.id_bus = (int)reader[0];
                            bus.Number = (int)reader[1];
                            bus.Class = (string)reader[2];
                            bus.no_seat = (int)reader[3];
                            bus.isDeleted = (bool)reader[4];
                            buses[numOfbuses++] = bus;
                            numOfTokenSeats.Add((int)reader[5]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message: {ex.Message},   {ex.StackTrace}");
            }
        }
        public static void getFreeBusesInThatDay(DateTime date, out Bus[] buses, ref int numOfbuses)
        {
            buses=new Bus[50];
            numOfbuses = 0;
            try
            {
                string query = $"SELECT        Id_bus, Number, Class, no_seats, is_deleted FROM            dbo.bus WHERE        (is_deleted = 0) AND (Id_bus NOT IN  (SELECT        id_bus  FROM            dbo.Travel     WHERE        (is_deleted = 0) AND (Date = '{date.ToString("yyyy-MM-dd")}')   GROUP BY id_bus))";
                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Bus bus = new Bus();
                            bus.id_bus = (int)reader[0];
                            bus.Number = (int)reader[1];
                            bus.Class = (string)reader[2];
                            bus.no_seat = (int)reader[3];
                            bus.isDeleted = (bool)reader[4];
                            buses[numOfbuses++] = bus;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message: {ex.Message},   {ex.StackTrace}");
            }
        }

        // insert a stop
        public static void InsertStop(Stops stop)
        {
            string query = "INSERT INTO Stop VALUES (@id_Stop, @city, @address,@is_deleted)";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    ("@id_Stop", stop.Id), // generate id not handled
                    ("@city", stop.City),
                    ("@address", stop.Address),
                     ("@is_deleted", false)

                );

                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Stop Insert Exception: {ex.Message}");
            }
        }

        //display stops
        public static void getAllStops(out Stops [] stops,ref int numOfStops)
        {
            string query = "SELECT * FROM Stop Where is_deleted=0";
            numOfStops = 0;
            stops = new Stops[50];
            try
            {
                using (SqlConnection con = GetOpenConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable stopsTable = new DataTable();
                    adapter.Fill(stopsTable);
                    numOfStops = 0;
                    foreach (DataRow row in stopsTable.Rows)
                    {
                        Stops stop=new Stops();
                        stop.Id =(int) row["Id_Stop"];
                        stop.City = (string)row["City"];
                        stop.Address = (string)row["Address"];
                        stop.Is_deleted=(bool)row["Is_deleted"];
                        stops[numOfStops++] = stop; 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Show Stops Exception: {ex.Message}");
            }
        }

        // update stop

        public static void UpdateStop(int stopId, Stops updatedStop)
        {
            // Check if the stop is deleted before attempting to update
            if (IsStopDeleted(stopId))
            {
                Console.WriteLine("Cannot update a deleted stop.");
                return;
            }


            string query = $"UPDATE Stop SET city = @city, address = @address WHERE id_Stop = @id_Stop";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    ("@id_Stop", stopId),
                    ("@city", updatedStop.City),
                    ("@address", updatedStop.Address)

                );

                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Stop Exception: {ex.Message}");
            }
        }

        // delete stop
        public static void DeleteStop(int stopId)
        {
            // Check if the stop is already deleted
            if (IsStopDeleted(stopId))
            {
                Console.WriteLine("Stop is already deleted.");
                return;

            }

            string query = $"UPDATE Stop SET is_deleted = 1 WHERE id_Stop = @id_Stop ";
            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    ("@id_Stop", stopId)


                );

                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Stop Exception: {ex.Message}");
            }


            //string query = $"DELETE FROM Stop WHERE id_Stop = @id_Stop";

            //try
            //{
            //    int rowsAffected = DatabaseHandler.ExecuteNonQuery(query, ("@id_Stop", stopId));

            //    Console.WriteLine($"Rows affected: {rowsAffected}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Delete Stop Exception: {ex.Message}");
            //}
        }

        // function to check stop is deleted or not

        private static bool IsStopDeleted(int stopId)
        {
            string query = $"SELECT is_deleted FROM Stop WHERE id_Stop = @id_Stop";

            try
            {
                using (SqlConnection con = GetOpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id_Stop", stopId);

                    object result = cmd.ExecuteScalar();
                    return result != null && (bool)result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Check if Stop is Deleted Exception: {ex.Message}");
                return true; // Assume stop is deleted in case of an exception
            }
        }
        public static void getallTripsEndsThatStartFrom(int start, out Stops[] stops, ref int numOfStops)

        {
            numOfStops = 0;
            stops = new Stops[50];
            try
            {
                string query = $"SELECT dbo.stop.Id_Stop, dbo.stop.City, dbo.stop.Address, dbo.stop.is_deleted FROM     dbo.trip INNER JOIN   dbo.stop ON dbo.trip.id_end = dbo.stop.Id_Stop WHERE  (dbo.trip.id_start = {start}) AND (dbo.trip.is_deleted = 0) AND (dbo.stop.is_deleted = 0) GROUP BY dbo.stop.Id_Stop, dbo.stop.City, dbo.stop.Address, dbo.stop.is_deleted";
                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    numOfStops = 0;
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Stops stop = new Stops();
                            stop.Id = (int)reader[0];
                            stop.City = (string)reader[1];
                            stop.Address = (string)reader[2];
                            stop.Is_deleted = (bool)reader[3];
                            stops[numOfStops++] = stop;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message: {ex.Message}");
            }

        }

        static public void InsertTrip(Trip trip)
        {
            string query = "insert into [dbo].[Trip]([Id_Trip], [Time], [Price], [id_start], [id_end], [isDeleted]) VALUES(@no_seat,@Date,@id_passenger,@id_bus,@id_trip)";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    ("@Id_Trip", trip.Id_Trip),
                    ("@Time", trip.Time),
                    ("@Price", trip.Price),
                    ("@id_start", trip.id_start),
                    ("@id_end", trip.id_end),
                    ("@is_deleted", trip.isDeleted)
                );

                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Driver Insert Exception: {ex.Message}");
            }
        }
        public static void DeleteTrip(int tripID)
        {
            string query = "update [dbo].[Trip] set [is_deleted] = 1 where [Id_Trip] = @tripID;";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query, ("@tripID", tripID));
                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Driver Insert Exception: {ex.Message}");
            }
        }

        static public void InsertBus(Bus bus)
        {
            string query = "insert into [dbo].[Bus]([id_bus], [Number], [Class], [no_seat], [isDeleted] VALUES(@id_bus,@Number,@Class,@no_seat,@isDeleted)";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query,
                    ("@id_bus", bus.id_bus),
                    ("@Class", bus.Class),
                    ("@no_seat", bus.no_seat),
                    ("@is_deleted", bus.isDeleted)
                );

                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Driver Insert Exception: {ex.Message}");
            }
        }
        public static void DeleteBus(int id_bus)
        {
            string query = "update [dbo].[Bus] set [is_deleted] = 1 where [id_bus] = @id_bus";

            try
            {
                int rowsAffected = DatabaseHandler.ExecuteNonQuery(query, ("@id_bus", id_bus));
                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Driver Insert Exception: {ex.Message}");
            }
        }

        internal static void GetAllBuses(Bus[] busesArray, ref int numOfBuses)
        {
            try
            {
                string query = "SELECT * FROM bus";

                using (SqlDataReader reader = ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            if (!(bool)reader["is_deleted"]) // if not deleted
                            {
                                Bus bus = new Bus();
                                bus.id_bus = (int)reader["id_bus"];
                                bus.Number = (int)reader["number"];
                                bus.Class = (string)reader["class"];
                                bus.isDeleted = (bool)reader["is_deleted"];

                                busesArray[counter++] = bus;
                            }
                        }
                        numOfBuses = counter;
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.DisplayErrorMessage($"Exception Message: {ex.Message}");
            }
        }
    }
}
