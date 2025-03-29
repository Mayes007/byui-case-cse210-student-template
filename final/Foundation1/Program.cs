using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }

    public void Display()
    {
        Console.WriteLine($"{CommenterName}: {Text}");
    }
}

class Video
{
    public string Title { get; set; }
    public string Uploader { get; set; }
    public int Length { get; set; }

    private List<Comment> Comments;

    public Video(string title, string uploader, int length)
    {
        Title = title;
        Uploader = uploader;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public void Display()
    {
        Console.WriteLine($"\nTitle: {Title}");
        Console.WriteLine($"Uploaded by: {Uploader}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Comments ({Comments.Count}):");

        foreach (var comment in Comments)
        {
            comment.Display();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Random rand = new Random();

        List<string> commenterNames = new List<string>
        {
            "MattatonFan89", "GoatMomProtector", "ChaoticNeutralDan",
            "PixelPunk27", "FroggitLover", "PapyrusRules", "SansTheMan",
            "AlphysArchive", "NapstablookMood", "FloweyWatcher", "DeterminedSoul", "UndyneUnstoppable"
        };

        List<string> commentTexts = new List<string>
        {
            "Still waiting for the musical number from Mettaton. MAKE IT HAPPEN.",
            "Not Dan accidentlly insulting Toriel and immediately regretting it.",
            "Dan's chaotic energy + Undertale's emotional plot = masterpiece.",
            "Phil’s reaction to Flowey was GOLD",
            "I cried when they reached the true lab... again.",
            "They better do the Genocide route next.",
            "I swear, every time Sans appears, the fandom loses it—and I'm one of them.",
            "Alphys would definitely be fangirling over this entire playthrough.",
            "I need a sequel where Dan becomes besties with Napstablook.",
            "The fact that Phil spared everyone… king behavior.",
            "Watching this is like reliving my first time playing Undertale.",
            "They missed so many secrets and I'm screaming internally lol"
        };

        Video video = new Video("Dan and Phil play UNDERTALE", "DanAndPhilGAMES", 3848);

        int numberOfComments = 5; // You can increase this if you want
        int maxComments = Math.Min(commenterNames.Count, commentTexts.Count);

        if (numberOfComments > maxComments)
            numberOfComments = maxComments;

        for (int i = 0; i < numberOfComments; i++)
        {
            int nameIndex = rand.Next(commenterNames.Count);
            int textIndex = rand.Next(commentTexts.Count);

            string name = commenterNames[nameIndex];
            string text = commentTexts[textIndex];

            video.AddComment(new Comment(name, text));

            commenterNames.RemoveAt(nameIndex);
            commentTexts.RemoveAt(textIndex);
        }

        video.Display();
    }
}
