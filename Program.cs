


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
enum PlacePreference
{
    Seated,
    Standing
}
class TicketOffice
{
    static Random random = new Random();
    static HashSet<int> usedPlaceNumbers = new HashSet<int>();
    static string placeList = ",";
    static int TicketNumberGenerator()
    {
        return random.Next(1, 8001);
    }
    static int PriceSetter(int age, PlacePreference placePreference)
    {
        int price = 0;
        if (age <= 11)
        {
            price = (placePreference == PlacePreference.Seated) ? 50 : 25;
        }
        else if (age >= 12 && age <= 64)
        {
            price = (placePreference == PlacePreference.Seated) ? 170 : 110;
        }
        else if (age >= 65)
        {
            price = (placePreference == PlacePreference.Seated) ? 100 : 60;
        }
        return price;
    }
    static decimal TaxCalculator(int price)
    {
        decimal tax = Math.Round((1 - 1 / 1.06m) * price, 2);
        return tax;
    }
    static bool CheckPlaceAvailability(int placeNumber)
    {
        return !usedPlaceNumbers.Contains(placeNumber);
    }
    static void AddPlace(int placeNumber)
    {
        usedPlaceNumbers.Add(placeNumber);
    }
    static void DisplayUsedPlaceNumbers()
    {
        Console.WriteLine("\nUsed Place Numbers:");
        if (usedPlaceNumbers.Count == 0)
        {
            Console.WriteLine(" There is no place number yet.");
        }
        else
        {
            foreach (int placeNumber in usedPlaceNumbers)
            {
                Console.Write($"{placeNumber}, ");
            }
            Console.WriteLine();
        }
    }
    static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE");
        Console.WriteLine("Ticket Office - Calculate Ticket Price");
        Console.WriteLine("--------------------------------------");
        bool continueProgram = true;
        while (continueProgram)
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Calculate Ticket");
            Console.WriteLine("2. Check Place Availability");
            Console.WriteLine("3. Display Used Place Numbers");
            Console.WriteLine("4. Exit");
            Console.Write("\nEnter your choice (1/2/3/4): ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("\nEnter your age: ");
                    int age;
                    if (!int.TryParse(Console.ReadLine(), out age) || age < 0 || age > 110)
                    {
                        Console.WriteLine(" Invalid age input.");
                        continue;
                    }
                    Console.Write("Enter ticket type (Seated or Standing): ");
                    string placeInput = Console.ReadLine().ToLower();
                    PlacePreference placePreference;
                    if (placeInput == "seated")
                    {
                        placePreference = PlacePreference.Seated;
                    }
                    else if (placeInput == "standing")
                    {
                        placePreference = PlacePreference.Standing;
                    }
                    else
                    {
                        Console.WriteLine(" Invalid place preference.");
                        continue;
                    }
                    int ticketPrice = PriceSetter(age, placePreference);
                    decimal tax = TaxCalculator(ticketPrice);
                    int ticketNumber = TicketNumberGenerator();
                    Console.WriteLine("\nReceipt:");
                    Console.WriteLine($" Age: {age}");
                    Console.WriteLine($" Ticket Type: {placeInput}");
                    Console.WriteLine($" Ticket Price: {ticketPrice.ToString("C")}");
                    Console.WriteLine($" Tax (6%): {tax.ToString("C")}");
                    Console.WriteLine($" Total Price: {(ticketPrice + tax).ToString("C")}");
                    Console.WriteLine($" Ticket Number: {ticketNumber}");
                    usedPlaceNumbers.Add(ticketNumber);
                    break;

                case "2":
                    Console.Write("\nEnter the place number to check availability: ");
                    int placeNumberToCheck;
                    if (int.TryParse(Console.ReadLine(), out placeNumberToCheck))
                    {
                        if (CheckPlaceAvailability(placeNumberToCheck))
                        {
                            Console.WriteLine($" Place {placeNumberToCheck} is available.");
                        }
                        else
                        {
                            Console.WriteLine($" Place {placeNumberToCheck} is already taken.");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Invalid place number input.");
                    }
                    break;

                case "3":
                    DisplayUsedPlaceNumbers();
                    break;

                case "4":
                    continueProgram = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option (1 / 2 / 3 / 4).");
                    break;
            }
        }
        Console.WriteLine("\nThank you for using the Ticket Office. Goodbye!");
    }
}

