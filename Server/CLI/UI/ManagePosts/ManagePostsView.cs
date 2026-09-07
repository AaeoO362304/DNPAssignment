using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private CreatePostView createPostView;
    private ListPostsView listPostsView;
    private SinglePostView singlePostView;
    private DeletePostView deletePostView;
    private UpdatePostView updatePostView;
    
    public ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }
    
    public void Options()
    {
        Console.WriteLine("1. Create Post");
        Console.WriteLine("2. View all Posts");
        Console.WriteLine("3. View Post");
        Console.WriteLine("4. Edit Post");
        Console.WriteLine("5. Delete Post");
        String? choice = Console.ReadLine();

        switch (choice)
        {
            case null : Console.WriteLine("Option doesnt exist.");
                break;
            case "1" :
                createPostView = new CreatePostView(postRepository);
                createPostView.CreatePost();
                break;
            case "2" :
                listPostsView = new ListPostsView(postRepository);
                listPostsView.ShowAllPosts();
                break;
            case "3" : 
                singlePostView = new SinglePostView(postRepository, commentRepository);
                singlePostView.ShowPost();
                break;
            case "4" :
                deletePostView = new DeletePostView(postRepository);
                deletePostView.deletePost();
                break;
            case "5" :
                updatePostView = new UpdatePostView(postRepository);
                updatePostView.updatePost();
                break;
            
        }
        
    }
}