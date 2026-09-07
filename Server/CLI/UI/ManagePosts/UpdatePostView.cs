using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class UpdatePostView
{
    private readonly IPostRepository postRepository;
    
    public UpdatePostView (IPostRepository postRepository)
        {
        this.postRepository = postRepository;
        }

    public async void updatePost()
    {
        Console.Write("Which Post you want to update?: ");
        int postID = int.Parse(Console.ReadLine());
        Post post = await postRepository.GetSingleAsync(postID);
        Console.WriteLine();
        Console.Write("New Post Title: ");
        String? title = Console.ReadLine();
        Console.WriteLine();
        Console.Write("New Post Body: ");
        String? body = Console.ReadLine();
        if (title != null) post.Title = title;
        if (body != null) post.Body = body;
        postRepository.UpdateAsync(post);
    }
}