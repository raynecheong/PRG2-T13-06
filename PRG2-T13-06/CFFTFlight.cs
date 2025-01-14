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

        public double CalculateFees()
        {
            return RequestFee= 450;
        }

        public override string ToString()
        {
            return base.ToString() + $", Fee: {RequestFee}";
        }
    }
}
