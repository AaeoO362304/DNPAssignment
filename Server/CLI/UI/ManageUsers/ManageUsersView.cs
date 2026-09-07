using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository _userRepository;

    public ManageUsersView(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }

    public void Options()
    {
        Console.WriteLine("1. Create User");
        Console.WriteLine("2. View All Users");
        Console.WriteLine("3. Edit User");
        Console.WriteLine("4. Delete User");
        
        String? choice =  Console.ReadLine();

        switch (choice)
        {
            case null : 
                Console.WriteLine("That option doesnt exist");
                break;
            case "1" :
                break;
            case "2" :
                break;
            case "3" :
                break;
            case "4" :
                break;
        }
    }
}