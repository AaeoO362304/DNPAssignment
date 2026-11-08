using Microsoft.AspNetCore.Mvc;
using ApiContracts;
using RepositoryContracts;

namespace WebApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    [HttpPost("login")]
    public ActionResult<UserDto> Login(
        [FromBody] LoginRequest request)
    {
        var user = userRepository.GetMany()
            .SingleOrDefault(u => u.UserName == request.UserName);

        if (user == null || user.Password != request.Password)
        {
            return Unauthorized();
        }

        UserDto userDto = new()
        {
            Id = user.Id,
            UserName = user.UserName
        };

        return Ok(userDto);
    }
}