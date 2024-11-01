using TapeHubDemo.Model;

namespace TapeHubDemo.View;

public partial class ShopBranchesPage : ContentPage
{
	public ShopBranchesPage() =>
		InitializeComponent();

    //
    // Summary:
    //     Event handler for when a branch is selected.
    //     Navigates to the «ProductsPage» with the selected branch ID.
    //     Clears the selection.
    private async void OnBranchSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ShopBranch selectedBranch)
        {
            await Shell.Current.GoToAsync($"//ProductsPage?branchId={selectedBranch.ID}");
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}