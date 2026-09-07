using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class DeletePostView
{
    private readonly IPostRepository postRepository;
    
    public DeletePostView (IPostRepository postRepository)
        {
        this.postRepository = postRepository;
        }

    public void deletePost()
    {
        Console.Write("Which Post you want to delete?: ");
        int postID = int.Parse(Console.ReadLine());
        postRepository.DeleteAsync(postID);
    }
}