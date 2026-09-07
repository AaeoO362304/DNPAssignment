using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class CreateComment
{
    private readonly ICommentRepository commentRepository;
    
    public  CreateComment(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public void postComment()
    {
        Console.Write("Under which post you want to comment?: ");
        int postID = int.Parse(Console.ReadLine());
        Console.WriteLine();
        Console.Write("Who are you (UserID): ");
        int userID = int.Parse(Console.ReadLine());
        Console.WriteLine();
        Console.Write("New Comment: ");
        String? body = Console.ReadLine();
        Comment comment = new Comment(body, postID, userID ); 
        
    }
}