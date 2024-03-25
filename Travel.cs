using System;

namespace bus_station
{
    class Travel
    {
        private  int id_travel ;

        public int Id_travel { get=>id_travel; set { id_travel = value; } }
        public string No_seat { get; set; }
        public DateTime Date { get; set; }

        public int Id_passenger { get; set; }
        public int Id_bus { get; set; }
        public int Id_trip { get; set; }
        public bool Is_deleted { get; set; }


        
        public Travel()
        {
           
        }

        public override string ToString()
        {
            return $"travel [Id={Id_travel},Seat Number ={No_seat}, Time ={Date} ,ID_Passenger ={Id_passenger}," +
                $"Id_bus ={Id_bus},Id_trip ={Id_trip},Is_deleted={Is_deleted}]";
        }
    }
}
