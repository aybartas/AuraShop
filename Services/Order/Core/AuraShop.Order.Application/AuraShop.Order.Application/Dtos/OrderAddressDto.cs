namespace AuraShop.Order.Application.Dtos;

public class OrderAddressDto
{
    public int? AddressId { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
}