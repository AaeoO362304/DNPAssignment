namespace ApiContracts;

public class CreatePostDto
{
    public required String Title { get; set; }
    public required String Body { get; set; }
    public required int UserId { get; set; }
}