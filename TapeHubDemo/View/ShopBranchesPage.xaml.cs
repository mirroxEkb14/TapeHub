using TapeHubDemo.Model;

namespace TapeHubDemo.View;

public partial class ShopBranchesPage : ContentPage
{
	public ShopBranchesPage()
	{
		InitializeComponent();
        ShopBranchesCollectionView.SelectionChanged += OnBranchSelected;
    }

    private async void OnBranchSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedBranch = e.CurrentSelection[0] as ShopBranch;
        if (selectedBranch != null)
            await Navigation.PushAsync(new ProductsPage(selectedBranch.ID));

        //if (e.CurrentSelection.FirstOrDefault() is ShopBranch selectedBranch)
        //    await Navigation.PushAsync(new ProductsPage(selectedBranch.ID));

        ((CollectionView)sender).SelectedItem = null;
    }
}