using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository userRepository;

    public DeleteUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void DeleteUser()
    {
        Console.WriteLine("Which user should be deleted?: ");
        int id =  int.Parse(Console.ReadLine());
        userRepository.DeleteAsync(id);
    }
}