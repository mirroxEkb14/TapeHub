namespace TapeHubDemo.ViewModel;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TapeHubDemo.Database;
using TapeHubDemo.Model;

public class UsersListPageViewModel : BaseViewModel
{
    public ObservableCollection<User> Users { get; set; }

    public UsersListPageViewModel()
    {
        Users = [];
        LoadUsers();
    }

    private async Task LoadUsers()
    {
        var userList = await UserService.GetAllUsersAsync();
        foreach (var user in userList)
            Users.Add(user);
    }
}
