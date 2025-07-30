using NadinSoft.Application.Contract.QueryResults.User;

namespace NadinSoft.Application.Contract.QueryResults.Product;

public class AllProductsByFilterQueryResult
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string ManufacturePhone { get; set; }
    public string ManufactureEmail { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime ProducedDate { get; set; }
    public GetUserByIdQueryResult User{ get; set; }
}