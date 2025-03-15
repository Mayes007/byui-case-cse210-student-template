using System;
using System.Collections.Generic;
using System.IO;

// Base Class
abstract class Goal
{
    protected string Name;
    protected int Points;
    protected bool IsComplete;
    
    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsComplete = false;
    }
    
    public abstract void RecordEvent();
    public abstract string DisplayGoal();
    public abstract string SaveGoal();
    public virtual int GetPoints() => Points;
    public bool GetCompletionStatus() => IsComplete;
}

// Simple Goal (One-time completion)
class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) {}
    
    public override void RecordEvent()
    {
        IsComplete = true;
        Console.WriteLine($"🎉 Goal '{Name}' completed! You earned {Points} points.");
    }
    
    public override string DisplayGoal() => $"[ {(IsComplete ? "X" : " ")} ] {Name}";
    public override string SaveGoal() => $"Simple,{Name},{Points},{IsComplete}";
}

// Eternal Goal (Repeating, never complete)
class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) {}
    
    public override void RecordEvent()
    {
        Console.WriteLine($"📖 Progress recorded for '{Name}'! You earned {Points} points.");
    }
    
    public override string DisplayGoal() => $"[∞] {Name}";
    public override string SaveGoal() => $"Eternal,{Name},{Points}";
}

// Checklist Goal (Complete X times, with a bonus at the end)
class ChecklistGoal : Goal
{
    private int TargetCount;
    private int CurrentCount;
    private int BonusPoints;
    
    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) 
        : base(name, points)
    {
        TargetCount = targetCount;
        CurrentCount = 0;
        BonusPoints = bonusPoints;
    }
    
    public override void RecordEvent()
    {
        CurrentCount++;
        if (CurrentCount >= TargetCount)
        {
            IsComplete = true;
            Console.WriteLine($"🏆 Goal '{Name}' fully completed! You earned {Points + BonusPoints} points.");
        }
        else
        {
            Console.WriteLine($"✅ Progress recorded for '{Name}'. You earned {Points} points. ({CurrentCount}/{TargetCount})");
        }
    }
    
    public override string DisplayGoal() => $"[ {(IsComplete ? "X" : CurrentCount + "/" + TargetCount)} ] {Name}";
    public override string SaveGoal() => $"Checklist,{Name},{Points},{TargetCount},{CurrentCount},{BonusPoints}";
}

// User Profile (Tracks goals and points)
class UserProfile
{
    private List<Goal> Goals = new List<Goal>();
    private int Score = 0;
    
    public UserProfile()
    {
        // Default Goals
        AddGoal(new SimpleGoal("Go on a walk", 10));
        AddGoal(new ChecklistGoal("Go to the temple", 50, 10, 500));
        AddGoal(new EternalGoal("Make your bed", 30));
        AddGoal(new EternalGoal("Go to school/class", 100));
    }
    
    public void AddGoal(Goal goal) => Goals.Add(goal);
    
    public void RecordEvent(int index)
    {
        if (index < Goals.Count)
        {
            Goal goal = Goals[index];
            goal.RecordEvent();
            Score += goal.GetPoints();
        }
    }
    
    public void DisplayGoals()
    {
        for (int i = 0; i < Goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Goals[i].DisplayGoal()}");
        }
        Console.WriteLine($"✨ Total Score: {Score}");
    }
    
    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(Score);
            foreach (Goal goal in Goals)
                writer.WriteLine(goal.SaveGoal());
        }
    }
}

// Main Program
class Program
{
    static void Main()
    {
        UserProfile user = new UserProfile();
        
        Console.WriteLine("🌟 Welcome to Eternal Quest 🌟");
        
        while (true)
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1️⃣ Add a New Goal");
            Console.WriteLine("2️⃣ View Goals");
            Console.WriteLine("3️⃣ Record Progress");
            Console.WriteLine("4️⃣ Exit");
            Console.Write("Enter your choice: ");
            
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Write("Enter goal name: ");
                string goalName = Console.ReadLine();
                
                Console.WriteLine("Select goal type: 1) Simple 2) Eternal 3) Checklist");
                string goalType = Console.ReadLine();
                
                Console.Write("Enter points for completion: ");
                int points = int.Parse(Console.ReadLine());
                
                if (goalType == "1")
                {
                    user.AddGoal(new SimpleGoal(goalName, points));
                }
                else if (goalType == "2")
                {
                    user.AddGoal(new EternalGoal(goalName, points));
                }
                else if (goalType == "3")
                {
                    Console.Write("Enter target count: ");
                    int targetCount = int.Parse(Console.ReadLine());
                    Console.Write("Enter bonus points for completion: ");
                    int bonusPoints = int.Parse(Console.ReadLine());
                    user.AddGoal(new ChecklistGoal(goalName, points, targetCount, bonusPoints));
                }
                
                Console.WriteLine("🎯 Goal added successfully!");
            }
            else if (choice == "2")
            {
                user.DisplayGoals();
            }
            else if (choice == "3")
            {
                Console.Write("Enter goal number: ");
                if (int.TryParse(Console.ReadLine(), out int goalNum))
                    user.RecordEvent(goalNum - 1);
            }
            else if (choice == "4")
            {
                Console.WriteLine("🚀 Keep working on your Eternal Quest! Goodbye!");
                break;
            }
        }
    }
}
