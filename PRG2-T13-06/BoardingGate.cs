using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_06
{
    internal class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate() { }
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
        }

        public double CalculateFees()
        {
            if (Flight is CFFTFlight && SupportsCFFT) { return Flight.CalculateFees() + 300; }
            else if (Flight is DDJBFlight && SupportsDDJB) { return Flight.CalculateFees() + 300; }
            else if (Flight is LWTTFlight && SupportsLWTT) { return Flight.CalculateFees() + 300; }
            else { return 0.0;  }
        }
        public override string ToString()
        {
            return $"Gate Name: {GateName}, DDJB: {SupportsDDJB}, CFFT: {SupportsCFFT}, LWTT: {SupportsLWTT}";
        }
    }
}
