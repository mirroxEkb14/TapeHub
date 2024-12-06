#region Imports
using CommunityToolkit.Mvvm.ComponentModel;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.View;
using TapeHubDemo.Utils;
#endregion

namespace TapeHubDemo.ViewModel;

public partial class ShopBranchesViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<ShopBranch> _branches;
    [ObservableProperty] private ShopBranch? _selectedBranch;
    [ObservableProperty] private bool _isAdmin;

    public ShopBranchesViewModel()
    {
        _branches = [];
        _isAdmin = false;
        LoadShopBranches();
    }

    private async Task LoadShopBranches()
    {
        var shopBranches = await ShopBranchService.GetAllShopBranchesAsync();
        Branches.Clear();

        foreach (var branch in shopBranches)
            Branches.Add(branch);
    }

    //
    // Summary:
    //     Updates the list of shop branches always when the «ShopBranchesPage» is navigated to.
    public async Task RefreshShopBranchesAsync() =>
        await LoadShopBranches();

    //
    // Summary:
    //     Is used in the «Frame.GestureRecognizers» logic to navigate to the «ProductsPage».
    [RelayCommand]
    public async Task BranchSelected(ShopBranch selectedBranch)
    {
        if (SelectedBranch != null)
        {
            ClearSelections();
            return;
        }
        await Shell.Current.GoToAsync(NavigationStateArguments.GetToProductsPageWithShopBranchIdIsAdmin(selectedBranch.ID, IsAdmin));
    }

    //
    // Summary:
    //     Is used in the «Frame.GestureRecognizers» logic to assign the selected branch for the Edit and Delete actions.
    //     Marks the current branch as selected.
    [RelayCommand]
    public void BranchDoublePressed(ShopBranch selectedBranch)
    {
        ClearSelections();

        selectedBranch.IsSelected = true;
        SelectedBranch = selectedBranch;
    }

    [RelayCommand]
    public static async Task AddBranchAsync() =>
        await Shell.Current.GoToAsync(NavigationStateArguments.ToAddBranchPage);

    [RelayCommand]
    public async Task EditBranchAsync()
    {
        if (SelectedBranch == null)
        {
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationEditBranch, MessageContainer.NoShopBranchSelected, AlertDisplayer.OK);
            return;
        }
        await Shell.Current.GoToAsync(NavigationStateArguments.GetToEditBranchPageWithShopBranchId(SelectedBranch.ID));
    }

    [RelayCommand]
    public async Task DeleteBranchAsync()
    {
        if (SelectedBranch == null)
        {
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationDeleteBranch, MessageContainer.NoShopBranchSelected, AlertDisplayer.OK);
            return;
        }

        var result = await ShopBranchService.DeleteShopBranchAsync(SelectedBranch.ID);
        if (result > 0)
        {
            Branches.Remove(SelectedBranch);
            ClearSelections();
            await AlertDisplayer.DisplayAlertAsync(AlertDisplayer.ValidationDeleteBranch, MessageContainer.SuccessMessageDeletingShopBranch, AlertDisplayer.OK);
        }
    }

    #region Private Helper Methods
    //
    // Summary:
    //     Clears previous selections when double clicked.
    private void ClearSelections()
    {
        foreach (var branch in Branches)
            branch.IsSelected = false;
        SelectedBranch = null;
    }
    #endregion
}
