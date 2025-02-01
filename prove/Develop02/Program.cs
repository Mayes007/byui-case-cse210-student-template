using System;
using System.Collections.Generic;
using System.IO;

public class Program {
    static void Main(string[] args) {
        Journal journal = new Journal();
        journal.Load();  // Load existing entries from file
        Menu.Display(journal);  // Show the menu
    }
}

public class Menu {
    private static List<string[]> dailyReflectionQuestions = new List<string[]>() {
        new string[] { "What made you smile today?", "What challenge did you overcome today?" },
        new string[] { "What did you learn today?", "How did you help others today?" },
        new string[] { "What are you thankful for today?", "What personal goal did you make progress on today?" }
    };

    public static void Display(Journal journal) {
        bool keepRunning = true;
        while (keepRunning) {
            Console.WriteLine("\nJournal Application");
            Console.WriteLine("1. Write new entry");
            Console.WriteLine("2. Read entries");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            switch (option) {
                case "1":
                    CreateEntry(journal);
                    break;
                case "2":
                    foreach (Entry entry in journal.Entries) {
                        entry.Display();
                    }
                    break;
                case "3":
                    journal.Save();
                    Console.WriteLine("Exiting and saving the journal.");
                    keepRunning = false;  // Set to false to exit the loop and application
                    continue;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }

            if (keepRunning) {  // Ask if continuing only if not exiting
                keepRunning = AskToContinue();
            }
        }
    }

    private static void CreateEntry(Journal journal) {
        Console.WriteLine("Select a template:");
        Console.WriteLine("1. Gratitude Journal");
        Console.WriteLine("2. Daily Reflection");
        Console.WriteLine("3. Free Form");
        Console.Write("Choose a template: ");
        string templateChoice = Console.ReadLine();
        List<string> lines = new List<string>();
        Random rnd = new Random();

        switch (templateChoice) {
            case "1":
                Console.WriteLine("What are three things you are grateful for today?");
                for (int i = 0; i < 3; i++) {
                    lines.Add(Console.ReadLine());
                }
                break;
            case "2":
                var questions = dailyReflectionQuestions[rnd.Next(dailyReflectionQuestions.Count)];
                foreach (var question in questions) {
                    Console.WriteLine(question);
                    lines.Add(Console.ReadLine());
                }
                break;
            case "3":
                Console.WriteLine("Write your thoughts (press ENTER to finish):");
                lines.Add(Console.ReadLine());
                break;
            default:
                Console.WriteLine("Invalid template choice.");
                return;
        }

        Entry newEntry = new Entry(DateTime.Now.ToString(), string.Join(Environment.NewLine, lines), templateChoice);
        journal.AddEntry(newEntry);
    }

    private static bool AskToContinue() {
        Console.WriteLine("Would you like to write another entry or do something else? (yes/no)");
        string response = Console.ReadLine().Trim().ToLower();
        return response == "yes" || response == "y";
    }
}

public class Journal {
    public List<Entry> Entries { get; set; }
    private const string FileName = "JournalEntries.txt";

    public Journal() {
        Entries = new List<Entry>();
    }

    public void AddEntry(Entry entry) {
        Entries.Add(entry);
    }

    public void Load() {
        if (File.Exists(FileName)) {
            string[] lines = File.ReadAllLines(FileName);
            foreach (string line in lines) {
                string[] parts = line.Split(new string[] { "||" }, StringSplitOptions.None);
                Entries.Add(new Entry(parts[0], parts[1], parts[2]));
            }
        }
    }

    public void Save() {
        List<string> lines = new List<string>();
        foreach (Entry entry in Entries) {
            lines.Add($"{entry.Date}||{entry.Text}||{entry.Type}");
        }
        File.WriteAllLines(FileName, lines);
    }
}

public class Entry {
    public string Date { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }  // Added to distinguish different types of entries

    public Entry(string date, string text, string type) {
        Date = date;
        Text = text;
        Type = type;
    }

    public void Display() {
        Console.WriteLine($"Date: {Date}\nType: {Type}\nText:\n{Text}\n");
    }
}