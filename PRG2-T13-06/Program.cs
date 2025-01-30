//==========================================================
// Student Number: S10267916E
// Student Name: Rayne Cheong Yun Hao
// Partner Name: Chin Wei Hong
//==========================================================
// See https://aka.ms/new-console-template for more information
using System.Globalization;

namespace PRG2_T13_06

{
    internal class Program
    {
        static Terminal terminal = new Terminal("Terminal 5");
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
            Console.WriteLine("0. Exit\n");
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
        //QN 2 (DONE)
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
        //QN 3 (DONE)
        static void DisplayFlights(Dictionary<string, Flight> flights)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Flights for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}Expected Departure/Arrival Time");
            foreach (KeyValuePair<string, Flight> kvp in flights)
            {
                Console.WriteLine($"{kvp.Value.FlightNumber,-16}{terminal.GetAirlineFromFlight(kvp.Value).Name,-23}{kvp.Value.Origin,-23}{kvp.Value.Destination,-23}{kvp.Value.ExpectedTime}");
            }

        }


        //QN 5 
        //static void CreateNewFlight(Dictionary<string, Flight> flights)
        //{
        //    Console.WriteLine("Enter Flight Number: ");
        //    string flightNum = Console.ReadLine();
        //    Console.WriteLine("Enter Origin: ");
        //    string origin = Console.ReadLine();
        //    Console.WriteLine("Enter Destination: ");
        //    string destination = Console.ReadLine();
        //    Console.WriteLine("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
        //    DateTime dateTime = DateTime.Parse(Console.ReadLine());
        //    Console.WriteLine("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        //    string request = Console.ReadLine();
        //    if (request == "CFFT")
        //    {
        //        CFFTFlight CFFT = new CFFTFlight(flightNum, origin, destination, dateTime, request, 0);
        //        double requestFee = CFFT.CalculateFees();
        //        CFFT.RequestFee = requestFee;
        //        flights.Add(flightNum, CFFT);
        //    }
        //    else if (request == "LWTT")
        //    {
        //        LWTTFlight LWTT = new LWTTFlight(flightNum, origin, destination, dateTime, request, 0);
        //        double requestFee = LWTT.CalculateFees();
        //        LWTT.RequestFee = requestFee;
        //        flights.Add(flightNum, LWTT);
        //    }
        //    else if (request == "DDJB")
        //    {
        //        DDJBFlight DDJB = new DDJBFlight(flightNum, origin, destination, dateTime, request, 0);
        //        double requestFee = DDJB.CalculateFees();
        //        DDJB.RequestFee = requestFee;
        //        flights.Add(flightNum, DDJB);
        //    }
        //    else
        //    {
        //        flights.Add(flightNum, new NORMFlight(flightNum, origin, destination, dateTime, request));
        //    }
        //}



        static void CreateNewFlight(Dictionary<string, Flight> flights)
        {
            Console.Write("Enter Flight Number: ");
            string flightNum = Console.ReadLine().ToUpper();

            // Check for duplicate flight number
            if (flights.ContainsKey(flightNum))
            {
                Console.WriteLine("Error: Flight number already exists. Please enter a different flight.");
                return;
            }

            Console.Write("Enter Origin: ");
            string origin = Console.ReadLine();

            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine();

            Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
            string inputDateTime = Console.ReadLine();
            if (!DateTime.TryParseExact(inputDateTime, "d/M/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                Console.WriteLine("Error: Invalid date format. Please use 'dd/mm/yyyy hh:mm'.");
                return;
            }

            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            string request = Console.ReadLine().ToUpper();

            List<string> validRequests = new List<string> { "CFFT", "DDJB", "LWTT", "NONE" };
            if (!validRequests.Contains(request))
            {
                Console.WriteLine("Error: Invalid special request code. Please enter CFFT, DDJB, LWTT, or None.");
                return;
            }

            Flight flight;
            if (request == "CFFT")
            {
                flight = new CFFTFlight(flightNum, origin, destination, dateTime, request, 0);
                ((CFFTFlight)flight).RequestFee = flight.CalculateFees();
            }
            else if (request == "DDJB")
            {
                flight = new DDJBFlight(flightNum, origin, destination, dateTime, request, 0);
                ((DDJBFlight)flight).RequestFee = flight.CalculateFees();
            }
            else if (request == "LWTT")
            {
                flight = new LWTTFlight(flightNum, origin, destination, dateTime, request, 0);
                ((LWTTFlight)flight).RequestFee = flight.CalculateFees();
            }
            else
            {
                flight = new NORMFlight(flightNum, origin, destination, dateTime, request);
            }

            flights.Add(flightNum, flight);
            Console.WriteLine($"\nFlight {flightNum} has been added successfully!");

            Console.Write("Would you like to add another flight? (Y/N): ");
            string anotherFlight = Console.ReadLine().ToUpper();
            if (anotherFlight == "Y")
            {
                CreateNewFlight(flights);
            }
        }



        static void DisplayAirlineFlights(Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Airline Code",-15} {"Airline Name"}");
            foreach (var airline in airlines.Values)
            {
                Console.WriteLine($"{airline.Code,-15} {airline.Name}");
            }

            Console.Write("Enter Airline Code: ");
            string airlineCode = Console.ReadLine()?.ToUpper();
            if (!airlines.ContainsKey(airlineCode))
            {
                Console.WriteLine("Invalid Airline Code. Please try again");
                return;
            }

            var airlineName = airlines[airlineCode].Name;
            List<Flight> airlineFlights = new List<Flight>();
            foreach (var flight in flights.Values)
            {
                if (flight.FlightNumber.StartsWith(airlineCode))
                {
                    airlineFlights.Add(flight);
                }
            }

            if (airlineFlights.Count == 0)
            {
                Console.WriteLine($"No flights found for {airlineName}.");
                return;
            }

            Console.WriteLine("=============================================");
            Console.WriteLine($"List of Flights for {airlineName}");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-25} {"Origin",-25} {"Destination",-25} {"Expected"}");
            Console.WriteLine("Departure/Arrival Time");

            foreach (var flight in airlineFlights)
            {
                Console.WriteLine($"{flight.FlightNumber,-15} {airlineName,-25} {flight.Origin,-25} {flight.Destination,-25} {flight.ExpectedTime}");
            }
        }

        //QN 4
        static void AssignGateToFlight(Dictionary<string, Flight> flights, Dictionary<string, Airline> airline )
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Assign a Boarding Gate to a Flight");
            Console.WriteLine("=============================================");
            Console.WriteLine("Enter Flight Number:");
            string flightNum = Console.ReadLine();
            Console.WriteLine("Enter Boarding Gate Name:");
            string gate = Console.ReadLine();
            if (flights.ContainsKey(flightNum))
            {
                var value = flights[flightNum];
                Console.WriteLine($"Flight Number: {value.FlightNumber}");
                Console.WriteLine($"Origin: {value.Origin}");
                Console.WriteLine($"Destination: {value.Destination}");
                Console.WriteLine($"Expected Time: {value.ExpectedTime}");
                Console.WriteLine($"Special Request Code: {value.Status}");
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

                if (input == "3") { AssignGateToFlight(flights, airlines); }
                if (input == "4") { CreateNewFlight(flights); }

                if (input == "5") { DisplayAirlines(airlines); }

            }
        }
    }
}