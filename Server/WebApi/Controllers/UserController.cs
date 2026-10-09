using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetUsers()
    {
        IEnumerable<UserDto> users = userRepository.GetMany()
            .Select(user => new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password
            });

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        User user = await userRepository.GetSingleAsync(id);

        if (user == null)
            return NotFound();

        UserDto userDto = new()
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = user.Password
        };

        return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser(
        [FromBody] CreateUserDto request)
    {
        User user = new(request.UserName, request.Password);

        User created = await userRepository.AddAsync(user);

        UserDto userDto = new()
        {
            Id = created.Id,
            UserName = created.UserName,
            Password = created.Password
        };

        return Created($"/users/{userDto.Id}", userDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUser(
        int id, [FromBody] CreateUserDto request)
    {
        User existing = await userRepository.GetSingleAsync(id);

        if (existing == null)
            return NotFound();

        User updated = new(request.UserName, request.Password)
        {
            Id = id
        };

        await userRepository.UpdateAsync(updated);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        User existing = await userRepository.GetSingleAsync(id);

        if (existing == null)
            return NotFound();

        await userRepository.DeleteAsync(id);

        return NoContent();
    }
}