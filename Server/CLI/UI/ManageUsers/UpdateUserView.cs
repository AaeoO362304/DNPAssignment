using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class UpdateUserView
{
    private readonly IUserRepository userRepository;

    public UpdateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async void updateUser()
    {
        Console.Write("Which user should be updated?: ");
        int id = int.Parse(Console.ReadLine());
        User user = await userRepository.GetSingleAsync(id);
        Console.WriteLine();
        Console.Write("New User Name: ");
        String?  name = Console.ReadLine();
        Console.WriteLine();
        Console.Write("New User Password: ");
        String?  password = Console.ReadLine();
        if (name != null) user.UserName = name;
        if (password != null) user.Password = password;
        userRepository.UpdateAsync(user);
    }
}