using CommunityToolkit.Mvvm.ComponentModel;
using TapeHubDemo.Database;
using TapeHubDemo.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using TapeHubDemo.View;
using TapeHubDemo.Control;

namespace TapeHubDemo.ViewModel;

public partial class ShopBranchesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ShopBranch> _branches = [];
    [ObservableProperty]
    private ShopBranch? _selectedBranch;
    [ObservableProperty]
    private bool _isAdmin;

    public ShopBranchesViewModel()
    {
        _branches = [];
        LoadShopBranches();
        _isAdmin = false;
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
        await Shell.Current.GoToAsync($"ProductsPage?branchId={selectedBranch.ID}");
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
    public async Task AddBranchAsync() =>
        await Shell.Current.GoToAsync($"{nameof(AddBranchPage)}");

    [RelayCommand]
    public async Task EditBranchAsync()
    {
        if (SelectedBranch == null)
        {
            await AlertDisplayer.DisplayAlertAsync("Edit Branch", "Please select a branch to edit.", "OK");
            return;
        }
        await Shell.Current.GoToAsync($"{nameof(EditBranchPage)}?shopBranchId={SelectedBranch.ID}");
    }

    [RelayCommand]
    public async Task DeleteBranchAsync()
    {
        if (SelectedBranch == null)
        {
            await AlertDisplayer.DisplayAlertAsync("Delete Branch", "Please select a branch to delete.", "OK");
            return;
        }

        var result = await ShopBranchService.DeleteShopBranchAsync(SelectedBranch.ID);
        if (result > 0)
        {
            Branches.Remove(SelectedBranch);
            ClearSelections();
            await AlertDisplayer.DisplayAlertAsync("Delete Branch", "Selected branch was successfully removed.", "OK");
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
