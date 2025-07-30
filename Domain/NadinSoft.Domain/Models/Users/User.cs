using NadinSoft.Domain.Models.Products;

namespace NadinSoft.Domain.Models.Users;

public class User:BaseEntity<long>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public IEnumerable<Product> Products { get; set; }
}