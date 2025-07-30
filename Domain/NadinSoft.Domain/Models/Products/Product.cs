using NadinSoft.Domain.Models.Users;

namespace NadinSoft.Domain.Models.Products;

public class Product : BaseEntity<long>
{
    public string Name { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime ProducedDate { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}