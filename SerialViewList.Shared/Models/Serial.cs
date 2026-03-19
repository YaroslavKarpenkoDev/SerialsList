
using SQLite;

namespace SerialViewList.Shared.Models;

public class Serial
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int? Rating { get; set; }
    
    [MaxLength(500)]
    public string? Name { get; set; }
    
    public int? Season { get; set; }
    
    public int? Episode { get; set; }
    
    public DateTime? LastSeen { get; set; } =  DateTime.Now;
}