using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_06
{
    internal class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }
        public CFFTFlight() { }
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            double totalFee = 150;
            if (Destination == "Singapore (SIN)") { totalFee += 500; }
            if (Origin == "Singapore (SIN)") { totalFee += 800; }

            double discount = 0;
            if (ExpectedTime.TimeOfDay < DateTime.Parse("11:00 am").TimeOfDay) { discount += 110; }
            if (ExpectedTime.TimeOfDay < DateTime.Parse("9:00 pm").TimeOfDay) { discount += 110; }
            if (Origin == "Dubai (DXB)" || Origin == "Bangkok (BKK)" || Origin == "Tokyo (NRT)") { discount += 25; }

            return totalFee - discount;
        }

        public override string ToString()
        {
            return base.ToString() + $", Fee: {RequestFee}";
        }
    }
}
