using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_station
{
    class Trip
    {
        public int Id_Trip { get; set; }
        public TimeSpan Time { get; set; }
        public Double Price { get; set; }
        public int id_start { get; set; }
        public int id_end  { get; set; }
        public bool  isDeleted { get; set; }

    }
}
