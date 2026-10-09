using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

[ApiController]
[Route("/posts")]
public class PostController : ControllerBase
{
    private readonly IPostRepository postRepository;

    public PostController(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PostDto>> GetPosts()
    {
        IEnumerable<PostDto> posts = postRepository.GetMany()
            .Select(post => new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                UserId = post.UserId
            });

        return Ok(posts);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostDto>> GetPost(int id)
    {
        Post post = await postRepository.GetSingleAsync(id);

        if (post == null)
            return NotFound();

        PostDto postDto = new()
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            UserId = post.UserId
        };

        return Ok(postDto);
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> AddPost(
        [FromBody] CreatePostDto request)
    {
        Post post = new(request.Title, request.Body, request.UserId);

        Post created = await postRepository.AddAsync(post);

        PostDto postDto = new()
        {
            Id = created.Id,
            Title = created.Title,
            Body = created.Body,
            UserId = created.UserId
        };

        return Created($"/posts/{postDto.Id}", postDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdatePost(
        int id, [FromBody] CreatePostDto request)
    {
        Post existing = await postRepository.GetSingleAsync(id);

        if (existing == null)
            return NotFound();

        Post updated = new(request.Title, request.Body, request.UserId)
        {
            Id = id
        };

        await postRepository.UpdateAsync(updated);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        Post existing = await postRepository.GetSingleAsync(id);

        if (existing == null)
            return NotFound();

        await postRepository.DeleteAsync(id);

        return NoContent();
    }
}