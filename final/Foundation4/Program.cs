using System;

public abstract class Activity
{
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }

    public Activity(DateTime date, int durationInMinutes)
    {
        Date = date;
        DurationInMinutes = durationInMinutes;
    }

    public abstract double GetDistance(); // in miles
    public abstract double GetSpeed();    // in mph
    public abstract double GetPace();     // in min per mile

    public virtual string GetSummary()
    {
        return $"{Date.ToShortDateString()} {this.GetType().Name} ({DurationInMinutes} min): " +
               $"Distance: {GetDistance():0.00} miles, Speed: {GetSpeed():0.00} mph, Pace: {GetPace():0.00} min/mile";
    }
}

public class Running : Activity
{
    public double Distance { get; set; }

    public Running(DateTime date, int duration, double distance)
        : base(date, duration)
    {
        Distance = distance;
    }

    public override double GetDistance() => Distance;
    public override double GetSpeed() => (Distance / DurationInMinutes) * 60;
    public override double GetPace() => DurationInMinutes / Distance;
}

public class Cycling : Activity
{
    public double Speed { get; set; }

    public Cycling(DateTime date, int duration, double speed)
        : base(date, duration)
    {
        Speed = speed;
    }

    public override double GetDistance() => Speed * DurationInMinutes / 60;
    public override double GetSpeed() => Speed;
    public override double GetPace() => 60 / Speed;
}

public class Swimming : Activity
{
    public int Laps { get; set; }

    public Swimming(DateTime date, int duration, int laps)
        : base(date, duration)
    {
        Laps = laps;
    }

    public override double GetDistance()
    {
        double meters = Laps * 50;
        double kilometers = meters / 1000;
        return kilometers * 0.62;
    }

    public override double GetSpeed() => (GetDistance() / DurationInMinutes) * 60;
    public override double GetPace() => DurationInMinutes / GetDistance();
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Exercise Tracker ===");
        Console.WriteLine("Choose activity type:");
        Console.WriteLine("1. Running");
        Console.WriteLine("2. Cycling");
        Console.WriteLine("3. Swimming");
        Console.Write("Enter your choice (1-3): ");
        string choice = Console.ReadLine();

        Console.Write("Enter date (yyyy-mm-dd): ");
        DateTime date = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter duration in minutes: ");
        int duration = int.Parse(Console.ReadLine());

        Activity activity = null;

        switch (choice)
        {
            case "1":
                Console.Write("Enter distance in miles: ");
                double runDistance = double.Parse(Console.ReadLine());
                activity = new Running(date, duration, runDistance);
                break;

            case "2":
                Console.Write("Enter speed in mph: ");
                double speed = double.Parse(Console.ReadLine());
                activity = new Cycling(date, duration, speed);
                break;

            case "3":
                Console.Write("Enter number of laps (50m per lap): ");
                int laps = int.Parse(Console.ReadLine());
                activity = new Swimming(date, duration, laps);
                break;

            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        Console.WriteLine("\n--- Exercise Summary ---");
        Console.WriteLine(activity.GetSummary());
    }
}
