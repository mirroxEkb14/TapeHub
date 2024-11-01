using SQLite;

namespace TapeHubDemo.Model;

[Table("ShopBranch")]
public class ShopBranch
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
}
