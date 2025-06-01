namespace AuraShop.Shared.Services;

public interface IIdentityService
{
    public Guid UserId { get; }
    public string Username { get; }
}