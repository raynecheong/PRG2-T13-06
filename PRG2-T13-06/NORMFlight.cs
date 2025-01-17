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
            double totalFee = 300;
            if (Destination == "Singapore (SIN)") { totalFee += 500; }
            if (Origin == "Singapore (SIN)") { totalFee += 800; }

            double discount = 25;
            if (ExpectedTime.TimeOfDay < DateTime.Parse("11:00 am").TimeOfDay) { discount += 110; }
            if (ExpectedTime.TimeOfDay < DateTime.Parse("9:00 pm").TimeOfDay) { discount += 110; }
            if (Origin == "Dubai (DXB)") { discount += 25; }
            if (Origin == "Bangkok (BKK)") { discount += 25; }
            if (Origin == "Tokyo (NRT)") { discount += 25; }

            return totalFee - discount;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
