using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bus_station
{
     class Bus
    {
        public int id_bus { get; set; }
        public int Number { get; set; }
        public string Class { get; set; }
        public int no_seat { get; set; }
        public bool isDeleted { get; set; }
        public override string ToString()
        {
            return $"[Id={id_bus}, sign No={Number}, class ={Class}, capacity ={no_seat}]";
        }
    }
}
