using System;
using System.Collections.Generic;

// Comment class
class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    // commenter_init_()
    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }

    // display()
    public void Display()
    {
        Console.WriteLine($"{CommenterName}: {Text}");
}

// Video class demonstrating abstraction
class Video
{
    public string Title { get; set; }
    public string Uploader { get; set; }
    public int Length { get; set; } // in seconds

    private List<Comment> Comments; // hidden internal data (abstraction)

    // video_init_()
    public Video(string title, string uploader, int length)
    {
        Title = title;
        Uploader = uploader;
        Length = length;
        Comments = new List<Comment>();
    }

    // add_comment()
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // get_comment_count()
    public int GetCommentCount()
    {
        return Comments.Count;
    }

    // display()
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
        // Create a new video
        Video video = new Video("Dan and Phil play UNDERTALE", "DanAndPhilGAMES", 3848);
        // Add comments to the video
        video.AddComment(new Comment("MattatonFan89", "Still waiting for the musical number from Mettaton. MAKE IT HAPPEN."));
        video.AddComment(new Comment("GoatMomProtector", "Not Dan accidentlly insulting Toriel and immediatately regretting it"));
        video.AddComment(new Comment("ChaoticNeutralDan", "Dan's chaotic energy + Undertale's emoional plot= masterpiece"));

        // Display video information and comments
        video.Display();
    }
}
}

