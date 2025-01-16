using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_06
{
    internal class NORMFlight : Flight
    {
        public NORMFlight() { }
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
        { }

        public override double CalculateFees()
        {
            if (Origin == "Singapore") { double fees = 800; return fees; }
            else if (Destination == "Singapore") { double fees = 500; return fees; }
            else { return 0; }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
