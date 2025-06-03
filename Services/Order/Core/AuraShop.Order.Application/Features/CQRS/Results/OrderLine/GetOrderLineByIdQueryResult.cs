namespace AuraShop.Order.Application.Features.CQRS.Results.OrderLine;

public class GetOrderLineByIdQueryResult
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductTotalPrice { get; set; }
    public int OrderId { get; set; }
}