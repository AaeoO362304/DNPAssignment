using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;
    
    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void ShowAllUsers()
    {
        List<User> users = new List<User>(userRepository.GetMany()) ;
        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine(users[i]);
        }
    }
}