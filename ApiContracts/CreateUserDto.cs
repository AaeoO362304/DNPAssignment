namespace ApiContracts;

public class CreateUserDto
{
    public required String UserName { get; set; }
    public required String Password { get; set; }
}