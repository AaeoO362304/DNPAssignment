namespace ApiContracts;

public class UpdateUserDto
{
    public required String UserName { get; set; }
    public required String Password { get; set; }
}