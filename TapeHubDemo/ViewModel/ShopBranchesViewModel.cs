using CommunityToolkit.Mvvm.ComponentModel;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using System.Collections.ObjectModel;

namespace TapeHubDemo.ViewModel;

public partial class ShopBranchesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ShopBranch> _branches = [];

    public ShopBranchesViewModel() =>
        LoadShopBranches();

    //
    // Summary:
    //     Asynchronously loads shop branches from the DB class «ShopBranchService».
    //     Clears any existing branches in the collection and populates it with data from the DB.
    private async Task LoadShopBranches()
    {
        var shopBranches = await ShopBranchService.GetAllShopBranchesAsync();
        Branches.Clear();

        foreach (var branch in shopBranches)
            Branches.Add(branch);
    }
}
