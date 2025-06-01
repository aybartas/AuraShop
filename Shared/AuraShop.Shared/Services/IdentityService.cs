namespace AuraShop.Shared.Services;

public class IdentityService : IIdentityService
{
    public Guid UserId => Guid.Parse("test-user-id");
    public string Username => "test-user";
}