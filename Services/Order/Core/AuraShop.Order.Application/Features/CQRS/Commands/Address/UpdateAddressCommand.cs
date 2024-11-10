﻿namespace AuraShop.Order.Application.Features.CQRS.Commands.Address;

public class UpdateAddressCommand
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Detail { get; set; }
}