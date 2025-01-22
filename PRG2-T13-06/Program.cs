// See https://aka.ms/new-console-template for more information
namespace PRG2_T13_06
{
    internal class Program
    {
        static void DisplayMenu()
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("Welcome to Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine("1. List All Flights");
            Console.WriteLine("2. List All Boarding Gates");
            Console.WriteLine("3. Assign a Boarding Gate to a Flight");
            Console.WriteLine("4. Create Flight");
            Console.WriteLine("5. Display Airline Flights");
            Console.WriteLine("6. Modify Flight Details");
            Console.WriteLine("7. Display Flight Schedule");
            Console.WriteLine("0. Exit");
            Console.Write("Please select your option: ");
        }

        static Dictionary<string, Airline> LoadAirlines(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                string name = parts[0];
                string code = parts[1];

                Airline airline = new Airline(name, code);
                airlines[code] = airline;
            }

            return airlines;

        }

        static void DisplayAirlines(Dictionary<string, Airline> airlines)
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("List of Airlines");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Airline Code",-10} {"Airline Name"}");
            Console.WriteLine(new string('-', 30));
            foreach (var airline in airlines.Values)
            {
                Console.WriteLine($"{airline.Name, -25}{airline.Code,-20}");
            }
        }

        static Dictionary<string, BoardingGate> LoadBoardingGates(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                string gateName = parts[0];
                bool supportsDDJB = bool.Parse(parts[1]);
                bool supportsCFFT = bool.Parse(parts[2]);
                bool supportsLWTT = bool.Parse(parts[3]);

                BoardingGate gate = new BoardingGate(gateName, supportsDDJB, supportsCFFT, supportsLWTT);
                boardingGates[gateName] = gate;
            }

            return boardingGates;
        }

        static void DisplayBoardingGates(Dictionary<string, BoardingGate> boardingGates)
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("List of Boarding Gates");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Gate Name",-10} {"DDJB",-10} {"CFFT",-10} {"LWTT",-10}");
            Console.WriteLine(new string('-', 55));
            foreach (var gate in boardingGates.Values)
            {
                Console.WriteLine($"{gate.GateName,-10} {gate.SupportsDDJB,-10} {gate.SupportsCFFT,-10} {gate.SupportsLWTT,-10}");
            }
        }

        static  Dictionary<string, Flight> LoadFlights(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] info = lines[i].Split(',');
                string flightNum = info[0];
                string origin = info[1];
                string destination = info[2];
                DateTime espectedTime = DateTime.Parse(info[3]);
                string status = info[4];

                if (status == "CFFT")
                {
                    CFFTFlight CFFT = new CFFTFlight(flightNum, origin, destination, espectedTime, status, 0);
                    double requestFee = CFFT.CalculateFees();
                    CFFT.RequestFee = requestFee;
                    flights.Add(flightNum, CFFT);
                }
                else if (status == "LWTT")
                {
                    LWTTFlight LWTT = new LWTTFlight(flightNum, origin, destination, espectedTime, status, 0);
                    double requestFee = LWTT.CalculateFees();
                    LWTT.RequestFee = requestFee;
                    flights.Add(flightNum, LWTT);
                }
                else if (status == "DDJB")
                {
                    DDJBFlight DDJB = new DDJBFlight(flightNum, origin, destination, espectedTime, status, 0);
                    double requestFee = DDJB.CalculateFees();
                    DDJB.RequestFee = requestFee;
                    flights.Add(flightNum, DDJB);
                }
                else
                {
                    flights.Add(flightNum, new NORMFlight(flightNum, origin, destination, espectedTime, status));
                }
            }
            return flights;
        }

        static void DisplayFlights(Dictionary<string, Flight> flights)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Flights for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Flight Number",-16}{"Origin",-23}{"Destination",-23}Expected Departure/Arrival Time");
            foreach (KeyValuePair<string, Flight> kvp in flights)
            {
                Console.WriteLine($"{kvp.Value.FlightNumber,-16}{kvp.Value.Origin,-23}{kvp.Value.Destination,-23}{kvp.Value.ExpectedTime}");
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Loading Airlines...");
            var airlines = LoadAirlines("airlines.csv");
            Console.WriteLine($"{airlines.Count} Airlines Loaded!");

            Console.WriteLine("Loading Boarding Gates...");
            var boardingGates = LoadBoardingGates("boardinggates.csv");
            Console.WriteLine($"{boardingGates.Count} Boarding Gates Loaded!");

            Console.WriteLine("Loading Flights...");
            var flights = LoadFlights("flights.csv");
            Console.WriteLine($"{flights.Count} Flights Loaded!");

            while (true)
            {
                DisplayMenu();
                string input = Console.ReadLine();
                if (input == "0") { Console.WriteLine("Goodbye!"); break; }
                if (input == "1") { DisplayFlights(flights); }
                if (input == "2") { DisplayBoardingGates(boardingGates); }
                if (input == "5") { ; }
            }
        }
    }
}