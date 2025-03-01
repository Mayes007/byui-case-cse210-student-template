using System;
using System.Collections.Generic;
using System.Threading;

abstract class MindfulnessActivity
{
    protected int duration;
    public abstract void StartActivity();

    protected void DisplayStartingMessage(string name, string description)
    {
        Console.WriteLine($"\n{name}");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready...");
        ShowCountdown(3);
    }

    protected void DisplayEndingMessage(string name)
    {
        Console.WriteLine("\nWell done!");
        Console.WriteLine($"You completed {name} for {duration} seconds.");
        ShowCountdown(3);
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : MindfulnessActivity
{
    public override void StartActivity()
    {
        DisplayStartingMessage("Breathing Activity", "This activity helps you relax by guiding your breathing.");
        
        int elapsed = 0;
        while (elapsed < duration)
        {
            Console.Write("Breathe in... ");
            ShowCountdown(4);
            Console.Write("Breathe out... ");
            ShowCountdown(4);
            elapsed += 8;
        }
        
        DisplayEndingMessage("Breathing Activity");
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What did you learn from this experience?",
        "Would you handle the situation differently now? Why or why not?",
        "How has this experience shaped who you are today?"
    };

    public override void StartActivity()
    {
        DisplayStartingMessage("Reflection Activity", "This activity helps you reflect on meaningful moments in your life.");

        Random rand = new Random();
        List<string> selectedQuestions = new List<string>(questions); // Copy of questions to prevent modification
        int elapsed = 0;

        while (elapsed < duration)
        {
            // Select a random prompt
            string prompt = prompts[rand.Next(prompts.Count)];
            Console.WriteLine($"\nReflect on this: {prompt}");
            ShowCountdown(3);

            // Shuffle and ask unique questions
            ShuffleList(selectedQuestions);

            foreach (var question in selectedQuestions)
            {
                if (elapsed >= duration) break; // Stop if time is up
                
                Console.WriteLine($"\n{question}");
                Console.Write("Take some time to reflect and then type your response: ");
                Console.ReadLine(); // User enters their response

                elapsed += 5;
            }
        }

        DisplayEndingMessage("Reflection Activity");
    }

    private void ShuffleList(List<string> list)
    {
        Random rand = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]); // Swap elements
        }
    }
}

class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public override void StartActivity()
    {
        DisplayStartingMessage("Listing Activity", "This activity helps you list positive things in your life.");
        
        Random rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        ShowCountdown(3);
        
        List<string> responses = new List<string>();
        int elapsed = 0;
        while (elapsed < duration)
        {
            Console.Write("Enter an item: ");
            responses.Add(Console.ReadLine());
            elapsed += 3;
        }
        
        Console.WriteLine($"You listed {responses.Count} items.");
        DisplayEndingMessage("Listing Activity");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nMindfulness App");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();
            
            if (choice == "1") new BreathingActivity().StartActivity();
            else if (choice == "2") new ReflectionActivity().StartActivity();
            else if (choice == "3") new ListingActivity().StartActivity();
            else if (choice == "4") break;
            else Console.WriteLine("Invalid choice. Try again.");
        }
    }
}
