
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_station
{
    class Stops
    {
        private static int nextId = 1;

        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool Is_deleted { get; set; }

        public Stops()
        {

        }

        public override string ToString()
        {
            return $" City :{City} , Address:{Address}";
        }
    }
}
