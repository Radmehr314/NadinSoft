namespace NadinSoft.Domain.Models.User;

public class User:BaseEntity<long>
{
    public string Username { get; set; }
    public string Password { get; set; }
}