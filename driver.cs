namespace bus_station
{
    class Driver
    {
        private static int nextId = 1; 

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int BusId { get; set; }
        public bool IsDeleted { get; set; }

        public Driver()
        {
            Id = nextId++; 
        }

        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}, Address={Address}, BusId={BusId}]";
        }
    }
}
