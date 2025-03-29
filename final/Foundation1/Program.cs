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

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void Display()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Uploaded by: {Uploader}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Comments ({GetCommentCount()}):");
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
            "PixelPunk27", "FroggitLover", "PapyrusRules", "SansTheMan"
        };

        List<string> commentTexts = new List<string>
        {
            "Still waiting for the musical number from Mettaton. MAKE IT HAPPEN.",
            "Not Dan accidentlly insulting Toriel and immediately regretting it.",
            "Dan's chaotic energy + Undertale's emotional plot = masterpiece.",
            "Phil’s reaction to Flowey was GOLD.",
            "I cried when they reached the true lab... again.",
            "They better do the Genocide route next.",
            "I swear, every time Sans appears, the fandom loses it—and I'm one of them."
        };

        // Create a video
        Video video = new Video("Dan and Phil play UNDERTALE", "DanAndPhilGAMES", 3848);

        // Decide how many unique comments to add (up to the minimum of the two lists)
        int numberOfComments = 3;
        int maxComments = Math.Min(commenterNames.Count, commentTexts.Count);

        if (numberOfComments > maxComments)
            numberOfComments = maxComments;

        for (int i = 0; i < numberOfComments; i++)
        {
            // Pick a random commenter and remove them from the list
            int nameIndex = rand.Next(commenterNames.Count);
            string commenter = commenterNames[nameIndex];
            commenterNames.RemoveAt(nameIndex);

            // Pick a random comment and remove it from the list
            int textIndex = rand.Next(commentTexts.Count);
            string text = commentTexts[textIndex];
            commentTexts.RemoveAt(textIndex);

            video.AddComment(new Comment(commenter, text));
        }

        // Display the video info and comments
        video.Display();
    }
}

