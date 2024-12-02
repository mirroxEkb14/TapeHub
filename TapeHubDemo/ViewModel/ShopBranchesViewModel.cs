using CommunityToolkit.Mvvm.ComponentModel;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace TapeHubDemo.ViewModel;

public partial class ShopBranchesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ShopBranch> _branches = [];

    public ShopBranchesViewModel() =>
        LoadShopBranches();

    private async Task LoadShopBranches()
    {
        var shopBranches = await ShopBranchService.GetAllShopBranchesAsync();
        Branches.Clear();

        foreach (var branch in shopBranches)
            Branches.Add(branch);
    }

    //
    // Summary:
    //     Is used in the «Frame.GestureRecognizers» logic to navigate to the «ProductsPage».
    [RelayCommand]
    public static async Task BranchSelected(ShopBranch selectedBranch)
    {
        if (selectedBranch != null)
            await Shell.Current.GoToAsync($"ProductsPage?branchId={selectedBranch.ID}");
    }
}
