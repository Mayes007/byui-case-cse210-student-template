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
    public int Length { get; set; } // in seconds
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
        Console.WriteLine($"Uploader: {Uploader}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine("Comments:");
        
        foreach (var comment in Comments)
        {
            comment.Display();
        }
    }

    }
