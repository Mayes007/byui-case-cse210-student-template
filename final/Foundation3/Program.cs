
using System;


public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public string Country { get; set; }
    public string Zipcode { get; set; }


    public string GetFullAddress()
    {
        return $"{Street}, {City}, {StateProvince}, {Country}, {Zipcode}";
    }
}


public class Event
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public Address Address { get; set; }


    public Event(string title, string description, DateTime date, TimeSpan time, Address address)
    {
        Title = title;
        Description = description;
        Date = date;
        Time = time;
        Address = address;
    }


    public virtual string GetStandardDetails()
    {
        return $"Title: {Title}\nDescription: {Description}\nDate: {Date.ToShortDateString()}\nTime: {Time}\nLocation: {Address.GetFullAddress()}";
    }


    public string GetShortDescription()
    {
        return $"{Title} on {Date.ToShortDateString()}";
    }


    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }
}


public class Lecture : Event
{
    public string SpeakerName { get; set; }
    public int Capacity { get; set; }


    public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speakerName, int capacity)
        : base(title, description, date, time, address)
    {
        SpeakerName = speakerName;
        Capacity = capacity;
    }


    public override string GetFullDetails()
    {
        return base.GetStandardDetails() + $"\nSpeaker: {SpeakerName}\nCapacity: {Capacity}";
    }
}


public class Reception : Event
{
    public string RsvpEmail { get; set; }


    public Reception(string title, string description, DateTime date, TimeSpan time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        RsvpEmail = rsvpEmail;
    }


    public override string GetFullDetails()
    {
        return base.GetStandardDetails() + $"\nRSVP Email: {RsvpEmail}";
    }
}


public class OutdoorGathering : Event
{
    public string WeatherStatement { get; set; }


    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address, string weatherStatement)
        : base(title, description, date, time, address)
    {
        WeatherStatement = weatherStatement;
    }


    public override string GetFullDetails()
    {
        return base.GetStandardDetails() + $"\nWeather Forecast: {WeatherStatement}";
    }
}


class Program
{
    static Address GetWaltonAddress()
    {
        return new Address
        {
            Street = "Walton's Mountain",
            City = "Spencer's Mill",
            StateProvince = "VA",
            Country = "USA",
            Zipcode = "24123"
        };
    }


    static void Main()
    {
        Console.WriteLine("=== Welcome to Walton's Event Planner ===");
        Console.WriteLine("Choose the type of event to plan:");
        Console.WriteLine("1. Lecture");
        Console.WriteLine("2. Reception");
        Console.WriteLine("3. Outdoor Gathering");


        Console.Write("Enter your choice (1-3): ");
        string choice = Console.ReadLine();


        Console.Write("Would you like to use a default Walton event? (y/n): ");
        string useDefault = Console.ReadLine().ToLower();


        Address waltonAddress = GetWaltonAddress();
        Event plannedEvent = null;


        if (useDefault == "y")
        {
            switch (choice)
            {
                case "1":
                    plannedEvent = new Lecture(
                        "Homesteading 101",
                        "Grandpa Zeb shares tips on farming and survival.",
                        new DateTime(1937, 10, 1),
                        new TimeSpan(6, 0, 0), //6:00 PM
                        waltonAddress,
                        "Zebulon Walton",
                        50
                    );
                    break;
                case "2":
                    plannedEvent = new Reception(
                        "Homecoming Supper",
                        "A family dinner to welcome John-Boy back home.",
                        new DateTime(1937, 10, 2),
                        new TimeSpan(5, 30, 0),//5:30 PM
                        waltonAddress,
                        "olivia@waltons.com"
                    );
                    break;
                case "3":
                    plannedEvent = new OutdoorGathering(
                        "Mountain Music Night",
                        "Bluegrass, lemonade, and stories under the stars.",
                        new DateTime(1937, 10, 3),
                        new TimeSpan(7, 0, 0), //7:00 PM
                        waltonAddress,
                        "Clear skies expected"
                    );
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
        }
        else
        {
            Console.Write("Enter the event title: ");
            string title = Console.ReadLine();


            Console.Write("Enter a short description: ");
            string description = Console.ReadLine();


            Console.Write("Enter the event date (yyyy-mm-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());


            Console.Write("Enter the event time (HH:mm): ");
            TimeSpan time = TimeSpan.Parse(Console.ReadLine());


            switch (choice)
            {
                case "1":
                    Console.Write("Enter the speaker's name: ");
                    string speaker = Console.ReadLine();
                    Console.Write("Enter the capacity: ");
                    int capacity = int.Parse(Console.ReadLine());


                    plannedEvent = new Lecture(title, description, date, time, waltonAddress, speaker, capacity);
                    break;


                case "2":
                    Console.Write("Enter RSVP email: ");
                    string rsvpEmail = Console.ReadLine();


                    plannedEvent = new Reception(title, description, date, time, waltonAddress, rsvpEmail);
                    break;


                case "3":
                    Console.Write("Enter the weather statement: ");
                    string weather = Console.ReadLine();


                    plannedEvent = new OutdoorGathering(title, description, date, time, waltonAddress, weather);
                    break;


                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
        }


        Console.WriteLine("\n--- Event Created Successfully ---");
        Console.WriteLine(plannedEvent.GetFullDetails());
        Console.WriteLine("\nShort Description: " + plannedEvent.GetShortDescription());
    }
}
