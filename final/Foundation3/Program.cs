using System;

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string StatePeovince { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {StatePeovince}, {Country}, {ZipCode}";
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
        return $"{Title}\n{Description}\nDate: {Date.ToShortDateString()}\nTime: {Time}\nAddress: {Address.GetFullAddress()}";
    }   
    public string GetShortDescription()
    {
        return $"{Title}\n{Date.ToShortDateString()}";
    }
    public virtual string GetFullDetails()
    {
        return $"{Title}\n{Description}\nDate: {Date.ToShortDateString()}\nTime: {Time}\nAddress: {Address.GetFullAddress()}";
    }
}
public class Lecture : Event
{
    public string SpeakerName { get; set; }
    public int Capacity { get; set; }

    public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speakerName, int capacity) : base(title, description, date, time, address)
    {
        SpeakerName = speakerName;
        Capacity = capacity;
    }
    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nSpeaker: {SpeakerName}\nCapacity: {Capacity}";
    }
}
public class Reception : Event
    {
        public string RSVPEmail { get; set; }

        public Reception(string title, string description, DateTime date, TimeSpan time, Address address, string rsvp) : base(title, description, date, time, address)
        {
            RSVPEmail = RSVPEmail;
        }
        public override string GetFullDetails()
        {
            return $"{base.GetFullDetails()}\nRSVP: {RSVPEmail}";
        }
    }
public class OutdoorGathering : Event
{
    public string WeatherForecast { get; set; }

    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address, string weatherForecast) : base(title, description, date, time, address)
    {
        WeatherForecast = weatherForecast;
    }
    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nWeather Forecast: {WeatherForecast}";
    }

}

class Program 
{
    static void Main()
    {
        Address address = new Address
        {
            Street = "Walton's Mountain",
            City = "Spencer's Mill",
            StatePeovince = "VA",
            Country = "USA",
            ZipCode = "24123"
        };

        DateTime date = new DateTime(1937, 10, 5);
        TimeSpan time = new TimeSpan(17, 00, 0); // 5:00 PM

        Lecture lecture= new Lecture(
            "Homesteading and Self-Reliance",
            "Grandpa Zeb will share his wisdom on farming, carpentry, gardening and mountain living.",
            date,
            time,
            address,
            "Zebulon Walton",
            40
        );
        Reception reception = new Reception(
            "Homecoming Supper",
            "A potluck reception to welcome back John-Boy from Boatwright University.",
            date.AddDays(1),
            time,
            address,
            "olivia.walton@waltonsmountain.com"
        );
        OutdoorGathering outdoorGathering = new OutdoorGathering(
            "Mountain Muisic Jam",
            "Join the neighbors for a night of music and fun under the stars.",
            date.AddDays(2),
            new TimeSpan(19, 00, 0), // 7:00 PM
            address,
            "Clear skies expected with a cool breeze."
        );
        Console.WriteLine("Lecture:\n" + lecture.GetFullDetails());
        Console.WriteLine("\nReception:\n" + reception.GetFullDetails());       
        Console.WriteLine("\nOutdoor Gathering:\n" + outdoorGathering.GetFullDetails());
        
    }
}