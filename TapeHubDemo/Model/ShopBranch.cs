using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace TapeHubDemo.Model;

//
// Summary:
//     This model class inherits from the «ObservableObject» class for containing the «_isSelected» property.
[Table("ShopBranch")]
public partial class ShopBranch : ObservableObject
{
    [Column("id"), PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [Column("name"), MaxLength(100)]
    public string? Name { get; set; }

    [Column("opening_hours")]
    public string? OpeningHours { get; set; }

    [Column("closing_hours")]
    public string? ClosingHours { get; set; }

    [Column("stock_quantity")]
    public int StockQuantity { get; set; }

    [Column("image_path")]
    public string? ImagePath { get; set; }

    [Column("contact_info_id")]
    public int ContactInfoID { get; set; }

    //
    // Summary:
    //     Is used to determine whether the branch is selected or not.
    //     Is needed to manage the selection of the branches for admins in the «ShopBranchesViewModel» class.
    [ObservableProperty]
    private bool _isSelected;
}
