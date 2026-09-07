using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    
    public  SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async void ShowPost()
    {
        Console.WriteLine("Which post you want to view?");
        int postID = int.Parse(Console.ReadLine());
        Post post = await postRepository.GetSingleAsync(postID);
        List<Comment> comments =
            new List<Comment>(commentRepository.GetManyFromPost(postID));
        Console.WriteLine(post);
        for (int i=0; i < comments.Count; i++)
        {
            Console.WriteLine(comments[i]);
        }
        
    }
}