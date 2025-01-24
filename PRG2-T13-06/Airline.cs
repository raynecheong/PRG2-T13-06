//==========================================================
// Student Number: S10267916E
// Student Name: Rayne Cheong Yun Hao
// Partner Name: Chin Wei Hong
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_06
{
    internal class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Airline() { }
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
            Flights = new Dictionary<string, Flight>();
        }

        public bool AddFlight(Flight flight)
        {
            if (!Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Add(flight.FlightNumber, flight);
                return true;
            }
            return false;
        }
        public bool RemoveFlight(Flight flight) { return Flights.Remove(flight.FlightNumber); }

        public double CalculateFees()
        {
            double totalFees = 0;
            foreach (var flight in Flights.Values)
            {
                totalFees += flight.CalculateFees();
            }

            int totalFlight = Flights.Count;
            int discount = totalFlight / 3;
            double totalDiscount = discount * 350;

            if (totalFlight > 5) { totalDiscount += totalFees * 0.03; }
            return totalFees - totalDiscount;
        }

        public override string ToString()
        {
            return $"{Name} ({Code}) has {Flights.Count} flight(s)";
        }
    }
}
