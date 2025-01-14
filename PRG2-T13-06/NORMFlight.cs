﻿using System;
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

        public double CalculateFees()
        {
            return 300;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
