using Systems;
using System.Collections.Generic;

namespace JournalApp
{
    public class Journal
    {
        private List<string>entries;
    }
    public void AddEntry(string entry)
    {
        entries.Add(entry);
    }

    public void RemoveEntry(int index)
    {
       if (index >= 0 && index < entries.Count)
         {
              entries.RemoveAt(index);
         }
    }
    else
    {
        console.WriteLine("Invalid index.");
    }
}
public void DisplayEntries()
{
    for (int i = 0; i < entries.Count; i++)
    {
        console.WriteLine($"{i+1}. {entries[i]}");
        {
            Console.writeLine($"{i+1}. {entries[i]}");
        }
    }
}