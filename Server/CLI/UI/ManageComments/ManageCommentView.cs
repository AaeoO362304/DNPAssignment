using CLI.UI.ManagePosts;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentView
{
    private readonly ICommentRepository commentRepository;
    private CreateCommentView createCommentView;

    public ManageCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public void Options()
    {
        Console.WriteLine("1. Create comment");
        String?  choice = Console.ReadLine();
        switch (choice)
        {
            case null : 
                Console.WriteLine("That option doesnt exist");
                break;
            case "1"  :
                createCommentView = new CreateCommentView(commentRepository);
                createCommentView.postComment();
                break;
        }
    }
}