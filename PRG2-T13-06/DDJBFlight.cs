﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_06
{
    internal class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight() { }
        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            RequestFee = 300;
            {
                if (Origin == "Singapore") { return RequestFee + 800; }
                else if (Destination == "Singapore") { return RequestFee + 500; }
                else { return RequestFee; }
            }
        }

        public override string ToString()
        {
            return base.ToString() + $", Fee: {RequestFee}";
        }
    }
}
