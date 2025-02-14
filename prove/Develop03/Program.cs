using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Private class for Word
    private class Word
    {
        public string Text { get; set; }
        public bool IsHidden { get; set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public string Display()
        {
            return IsHidden ? "____" : Text; // Display blank for hidden words
        }
    }

    // Private class for Reference (Scripture Reference)
    private class Reference
    {
        public string Verse { get; set; }

        // Constructor for single verse
        public Reference(string verse)
        {
            Verse = verse;
        }

        // Constructor for verse range
        public Reference(string startVerse, string endVerse)
        {
            Verse = $"{startVerse}-{endVerse}";
        }

        public override string ToString()
        {
            return Verse;
        }
    }

    // Private class for Scripture (Scripture with reference and words)
    private class Scripture
    {
        public Reference Reference { get; set; }
        public List<Word> Words { get; set; }

        public Scripture(Reference reference, string scriptureText)
        {
            Reference = reference;
            Words = new List<Word>();
            foreach (var word in scriptureText.Split(' '))
            {
                Words.Add(new Word(word));
            }
        }

        public void DisplayScripture()
        {
            Console.Clear();
            Console.WriteLine(Reference.ToString());
            foreach (var word in Words)
            {
                Console.Write(word.Display() + " ");
            }
            Console.WriteLine();
        }

        public void HideRandomWord()
        {
            Random random = new Random();
            var visibleWords = Words.FindAll(w => !w.IsHidden);
            if (visibleWords.Count > 0)
            {
                int randomIndex = random.Next(visibleWords.Count);
                visibleWords[randomIndex].IsHidden = true;
            }
        }

        public bool AllWordsHidden()
        {
            return Words.All(w => w.IsHidden);
        }
    }

    // Main method
    static void Main(string[] args)
    {
        // Example scripture text
 string scriptureText ="But, behold, I say unto you, that you must study it out in your mind; then you must ask me if it be right, and if it is right I will cause that your bosom shall burn within you; therefore, you shall feel that it is right.";
        
        var reference = new Reference("Doctine and Covenants 9:8");
        
        var scripture = new Scripture(reference, scriptureText);

        // Display full scripture first
        scripture.DisplayScripture();

        // Main loop for user input
        while (true)
        {
            Console.WriteLine("\nPress Enter to hide a word, or type 'quit' to end.");
            var userInput = Console.ReadLine();
            
            if (userInput.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWord();
            scripture.DisplayScripture();

            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("\nAll words have been hidden. Program will end.");
                break;
            }
        }
    }
}
