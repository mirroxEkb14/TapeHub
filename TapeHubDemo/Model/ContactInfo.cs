using SQLite;

namespace TapeHubDemo.Model;

[Table("ContactInfos")]
public class ContactInfo
{
    [Column("id"), PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    [Column("email"), MaxLength(100)]
    public string? Email { get; set; }
    [Column("phone"), MaxLength(15)]
    public string? Phone { get; set; }
    [Column("location_id")]
    public int? LocationID { get; set; }
}
