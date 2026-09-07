using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void createUser()
    {
        Console.Write("Username: ");
        String username = Console.ReadLine();
        Console.WriteLine();
        Console.Write("Password: ");
        String password = Console.ReadLine();
        User user = new User(username, password);
        userRepository.AddAsync(user);
    }
}