using TapeHubDemo.ViewModel;

namespace TapeHubDemo.View;

public partial class UsersListPage : ContentPage
{
    public UsersListPage()
    {
        InitializeComponent();
        BindingContext = new UsersListPageViewModel();
    }
}
