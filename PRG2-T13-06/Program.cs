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
                terminal.AddAirline(airline);
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
            Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}Expected Departure/Arrival Time");
            foreach (KeyValuePair<string, Flight> kvp in flights)
            {
                Console.WriteLine($"{kvp.Value.FlightNumber,-16}{terminal.GetAirlineFromFlight(kvp.Value).Name,-23}{kvp.Value.Origin,-23}{kvp.Value.Destination,-23}{kvp.Value.ExpectedTime}");
            }

        }





        static void CreateNewFlight(Dictionary<string, Flight> flights, Dictionary<string, Airline> airlines)
        {

            Console.Write("Enter Flight Number: ");
            string flightNumber = Console.ReadLine()?.ToUpper();

            if (flights.ContainsKey(flightNumber))
            {
                Console.WriteLine("Flight Number already exists. Please try again.");
                return;
            }

            Console.Write("Enter Origin: ");
            string origin = Console.ReadLine();
            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine();
            Console.Write("Enter Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
            DateTime expectedTime = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            string specialRequestCode = Console.ReadLine()?.ToUpper();

            Flight newFlight;
            if (specialRequestCode == "CFFT")
            {
                newFlight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "CFFT", 0);
            }
            else if (specialRequestCode == "DDJB")
            {
                newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "DDJB", 0);
            }
            else if (specialRequestCode == "LWTT")
            {
                newFlight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "LWTT", 0);
            }
            else
            {
                newFlight = new NORMFlight(flightNumber, origin, destination, expectedTime, "On Time");
            }

            flights.Add(flightNumber, newFlight);
            Console.WriteLine($"Flight {flightNumber} has been added!");

            Console.WriteLine(expectedTime.ToString("dd/MM/yyyy hh:mm tt"));
            string flightCSVPath = "flights.csv";
            string flightCSVLine = $"{flightNumber},{origin},{destination},{expectedTime:dd/MM/yyyy hh:mm:tt},{specialRequestCode}";
            File.AppendAllText(flightCSVPath, flightCSVLine + "\r\n");


            Console.Write("Would you like to add another flight? (Y/N): ");
            string addAnother = Console.ReadLine()?.ToUpper();

            if (addAnother == "Y")
            {
                CreateNewFlight(flights, airlines);
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


        static void AssignBoardingGate(Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Assign a Boarding Gate to a Flight");
            Console.WriteLine("=============================================");
            Console.Write("Enter Flight Number: ");
            string flightNumber = Console.ReadLine()?.ToUpper();

            if (!flights.ContainsKey(flightNumber))
            {
                Console.WriteLine("Invalid Flight Number. Please try again.");
                return;
            }

            var selectedFlight = flights[flightNumber];
            Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
            Console.WriteLine($"Origin: {selectedFlight.Origin}");
            Console.WriteLine($"Destination: {selectedFlight.Destination}");
            Console.WriteLine($"Expected Time: {selectedFlight.ExpectedTime}");
            Console.WriteLine($"Special Request Code: {selectedFlight.Status}");

            Console.Write("Enter Boarding Gate Name: ");
            string gateName = Console.ReadLine()?.ToUpper();

            if (!boardingGates.ContainsKey(gateName))
            {
                Console.WriteLine("Invalid Boarding Gate. Please try again.");
                return;
            }
            var selectedGate = boardingGates[gateName];
            Console.WriteLine($"Boarding Gate Name: {gateName}");
            Console.WriteLine($"Supports DDJB: {selectedGate.SupportsDDJB}");
            Console.WriteLine($"Supports CFFT: {selectedGate.SupportsCFFT}");
            Console.WriteLine($"Supports LWTT: {selectedGate.SupportsLWTT}");
            Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
            string updateStatus = Console.ReadLine()?.ToUpper();
            if (updateStatus == "Y")
            {
                Console.WriteLine("1. Delayed");
                Console.WriteLine("2. Boarding");
                Console.WriteLine("3. On Time");
                Console.Write("Please select the new status of the flight: ");
                string statusOption = Console.ReadLine();

                if (statusOption == "1")
                {
                    selectedFlight.Status = "Delayed";
                }
                else if (statusOption == "2")
                {
                    selectedFlight.Status = "Boarding";
                }
                else if (statusOption == "3")
                {
                    selectedFlight.Status = "On Time";
                }
                else
                {
                    Console.WriteLine("Invalid option. Status remains unchanged.");
                }
            }


            Console.WriteLine($"Flight {selectedFlight.FlightNumber} has been assigned to Boarding Gate {gateName}!");
        }

        static Dictionary<string, string> specialRequestCodes = new Dictionary<string, string>();
        static Dictionary<string, string> boardingGates = new Dictionary<string, string>();
        static void ModifyFlightDetails(Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights)
        {
            DisplayAirlines(airlines);

            Console.Write("Enter Airline Code: ");
            string airlineCode = Console.ReadLine()?.ToUpper();

            if (!airlines.ContainsKey(airlineCode))
            {
                Console.WriteLine("Invalid Airline Code. Please try again.");
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

            Console.WriteLine($"List of Flights for {airlineName}");
            Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-25} {"Origin",-25} {"Destination",-25} {"Expected"}");
            Console.WriteLine("Departure/Arrival Time");
            foreach (var flight in airlineFlights)
            {
                Console.WriteLine($"{flight.FlightNumber,-15} {airlineName,-25} {flight.Origin,-25} {flight.Destination,-25} {flight.ExpectedTime}");
            }

            Console.Write("Choose an existing Flight to modify or delete (Enter Flight Number): ");
            string flightNumber = Console.ReadLine()?.ToUpper();
            if (!flights.ContainsKey(flightNumber))
            {
                Console.WriteLine("Invalid Flight Number. Please try again.");
                return;
            }

            var selectedFlight = flights[flightNumber];
            Console.WriteLine("1. Modify Flight");
            Console.WriteLine("2. Delete Flight");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine("1. Modify Basic Information");
                Console.WriteLine("2. Modify Status");
                Console.WriteLine("3. Modify Special Request Code");
                Console.WriteLine("4. Modify Boarding Gate");
                Console.Write("Choose an option: ");
                string modifyOption = Console.ReadLine();

                if (modifyOption == "1")
                {
                    Console.Write("Enter new Origin: ");
                    selectedFlight.Origin = Console.ReadLine();
                    Console.Write("Enter new Destination: ");
                    selectedFlight.Destination = Console.ReadLine();
                    Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                    selectedFlight.ExpectedTime = DateTime.Parse(Console.ReadLine());
                }
                else if (modifyOption == "2")
                {
                    Console.Write("Enter new Status: ");
                    selectedFlight.Status = Console.ReadLine();
                }
                else if (modifyOption == "3")
                {
                    Console.Write("Enter new Special Request Code (e.g., DDJB, CFFT, LWTT, or None): ");
                    string newSpecialRequestCode = Console.ReadLine()?.ToUpper();

                    if (newSpecialRequestCode == "DDJB" || newSpecialRequestCode == "CFFT" || newSpecialRequestCode == "LWTT" || newSpecialRequestCode == "NONE")
                    {
                        if (newSpecialRequestCode == "NONE")
                        {
                            if (specialRequestCodes.ContainsKey(flightNumber))
                            {
                                specialRequestCodes.Remove(flightNumber);
                                Console.WriteLine("Special Request Code removed.");
                            }
                            else
                            {
                                Console.WriteLine("No Special Request Code exists for this flight.");
                            }
                        }
                        else
                        {
                            specialRequestCodes[flightNumber] = newSpecialRequestCode;
                            Console.WriteLine($"Special Request Code updated to {newSpecialRequestCode}.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Special Request Code. Please enter DDJB, CFFT, LWTT, or None.");
                    }
                }
                else if (modifyOption == "4")
                {
                    Console.Write("Enter new Boarding Gate: ");
                    string newBoardingGate = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(newBoardingGate))
                    {
                        if (boardingGates.ContainsKey(flightNumber))
                        {
                            boardingGates[flightNumber] = newBoardingGate;
                        }
                        else
                        {
                            boardingGates.Add(flightNumber, newBoardingGate);
                        }
                        Console.WriteLine($"Boarding Gate updated to {newBoardingGate}.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Boarding Gate. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    return;
                }
                Console.WriteLine("Flight updated!");
            }
            else if (option == "2")
            {
                Console.Write("Are you sure you want to delete this flight? (Y/N): ");
                string confirm = Console.ReadLine()?.ToUpper();

                if (confirm == "Y")
                {
                    flights.Remove(flightNumber);
                    Console.WriteLine("Flight deleted!");
                }
                else
                {
                    Console.WriteLine("Delete operation canceled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
            Console.WriteLine("=============================================");
            Console.WriteLine("Updated Flight Details");
            Console.WriteLine("=============================================");
            DisplayFlights(flights);
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

                if (input == "3") { AssignBoardingGate(flights, boardingGates); }
                if (input == "4") { CreateNewFlight(flights, airlines); }

                if (input == "5") { DisplayAirlines(airlines); }
                if (input == "6") { ModifyFlightDetails(airlines,flights); }

            }
        }
    }
}