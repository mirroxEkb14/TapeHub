using SQLite;
using TapeHubDemo.Enumeration;

namespace TapeHubDemo.Model;

[Table("Users")]
public class User
{
    [Column("id"), PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [Column("username"), MaxLength(100)]
    public string Username { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("role")]
    public UserRole Role { get; set; }

    [Column("contact_info_id")]
    public int ContactInfoID { get; set; }
}
