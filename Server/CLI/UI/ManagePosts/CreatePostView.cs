using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void CreatePost()
    {
        Console.Write("Title: "); 
        String title = Console.ReadLine();
        Console.WriteLine();
        Console.Write("Body: "); 
        String body = Console.ReadLine();
        Post post = new Post(title, body, 1);
        postRepository.AddAsync(post);
    }
}