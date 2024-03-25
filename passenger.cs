using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bus_station
{
    class Passenger
    {
        private int id;
        private string name;
        private string phone;

        public int ID { get { return id; } }
        public string Name { get { return name; } }
        public string Phone { get { return phone; } }
        public Passenger(string _name, string _phone) : this(0, _name, _phone)
        {
        }
        public Passenger(int _id, string _name, string _phone)
        {
            id = _id;   
            name = _name;
            phone = _phone;
        }
        public override string ToString()
        {
            return $"Passenger : {name}     Phone number: {phone}";
        }
    }
    
}
