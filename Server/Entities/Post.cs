namespace Entities;

public class Post
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public int UserId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public List<Comment> Comments { get; set; } = new();

    // Constructor for Entity Framework
    private Post()
    {
    }

    public Post(string title, string body, int userId)
    {
        Title = title;
        Body = body;
        UserId = userId;
    }

    public override string ToString()
    {
        return "ID: " + Id
                      + ", Title: " + Title
                      + ", Body: " + Body;
    }
}