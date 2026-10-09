using System.Text.Json;
using ApiContracts;
using Entities;
using FileRepositories;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApi.Controllers;


using ApiContracts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/comments")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> AddComment(
        [FromBody] CreateCommentDto request)
    {
        Comment comment = new(
            request.Body,
            request.PostId,
            request.UserId
        );

        Comment created = await commentRepository.AddAsync(comment);

        CommentDto commentDto = new()
        {
            Id = created.Id,
            Body = created.Body,
            PostId = created.PostId,
            UserId = created.UserId
        };

        return Created($"/comments/{commentDto.Id}", commentDto);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<CommentDto>> GetComments()
    {
        IEnumerable<CommentDto> comments = commentRepository
            .GetMany()
            .Select(comment => new CommentDto
            {
                Id = comment.Id,
                Body = comment.Body,
                PostId = comment.PostId,
                UserId = comment.UserId
            });

        return Ok(comments);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDto>> GetComment(int id)
    {
        Comment comment = await commentRepository.GetSingleAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        CommentDto commentDto = new()
        {
            Id = comment.Id,
            Body = comment.Body,
            PostId = comment.PostId,
            UserId = comment.UserId
        };

        return Ok(commentDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateComment(
        int id,
        [FromBody] CreateCommentDto request)
    {
        Comment comment = await commentRepository.GetSingleAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        Comment updatedComment = new(
            request.Body,
            request.PostId,
            request.UserId
        );

        // Preserve the ID of the comment being updated.
        // This requires your Comment entity to support setting its ID.
        await commentRepository.UpdateAsync(updatedComment);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteComment(int id)
    {
        Comment comment = await commentRepository.GetSingleAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        await commentRepository.DeleteAsync(id);

        return NoContent();
    }


}