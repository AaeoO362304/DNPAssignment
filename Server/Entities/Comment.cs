namespace Entities;

public class Comment
{
    public int Id { get; set; }

    public string Body { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    // Navigation properties
    public Post Post { get; set; } = null!;
    public User User { get; set; } = null!;

    // Constructor for Entity Framework
    private Comment()
    {
    }

    public Comment(string body, int postId, int userId)
    {
        Body = body;
        PostId = postId;
        UserId = userId;
    }

    public override string ToString()
    {
        return "Body: " + Body
                        + ", UserId: " + UserId;
    }
}