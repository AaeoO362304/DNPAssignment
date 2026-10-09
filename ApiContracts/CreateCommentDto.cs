namespace ApiContracts;

public class CreateCommentDto
{
    public required String Body { get; set; }
    public required int PostId { get; set; }
    public required int UserId { get; set; }
}