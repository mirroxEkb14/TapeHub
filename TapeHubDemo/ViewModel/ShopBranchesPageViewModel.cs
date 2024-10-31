using System.Collections.ObjectModel;
using TapeHubDemo.Database;
using TapeHubDemo.Model;

namespace TapeHubDemo.ViewModel;

public class ShopBranchesPageViewModel : BaseViewModel
{
    public ObservableCollection<ShopBranch> Branches { get; set; }

    public ShopBranchesPageViewModel()
    {
        Branches = [];
        LoadBranches();
    }

    private async void LoadBranches()
    {
        var branches = await ShopBranchService.GetAllBranchesAsync();
        foreach (var branch in branches)
            Branches.Add(branch);
    }
}
