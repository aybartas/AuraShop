namespace AuraShop.Order.Application.Features.Order.Dtos;

public class AddressDto
{
    public int? AddressId { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
}