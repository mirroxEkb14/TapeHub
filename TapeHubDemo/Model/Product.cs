using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using TapeHubDemo.Enumeration;

namespace TapeHubDemo.Model;

[Table("Products")]
public partial class Product : ObservableObject
{
    [Column("id"), PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [Column("title"), MaxLength(100)]
    public string? Title { get; set; }

    [Column("description"), MaxLength(500)]
    public string? Description { get; set; }

    [Column("price")]
    public double Price { get; set; }

    [Column("type")]
    public ProductType Type { get; set; }

    [Column("image_path")]
    public string? ImagePath { get; set; }

    [Column("shop_branch_id")]
    public int ShopBranchID { get; set; }

    //
    // Summary:
    //     Is used to determine whether the product is selected or not.
    //     Is needed to manage the selection of the branches for admins in the «ProductsViewModel» class.
    [ObservableProperty]
    private bool _isSelected;
}
