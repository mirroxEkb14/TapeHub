using SQLite;

namespace TapeHubDemo.Model;

[Table("Locations")]
public class Location
{
    [Column("id"), PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [Column("country")]
    public string Country { get; set; }

    [Column("city")]
    public string City { get; set; }

    [Column("street")]
    public string Street { get; set; }

    [Column("number")]
    public int Number { get; set; }

    [Column("zip")]
    public string ZIP { get; set; }
}

