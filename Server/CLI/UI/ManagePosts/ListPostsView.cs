using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;
    
    public  ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void ShowAllPosts()
    {
        List<Post> posts = new List<Post>(postRepository.GetMany()) ;
        for (int i = 0; i < posts.Count; i++)
        {
            Console.WriteLine(posts[i]);
        }
    }
    
}