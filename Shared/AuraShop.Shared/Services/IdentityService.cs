namespace AuraShop.Shared.Services;

public class IdentityService : IIdentityService
{
    public Guid UserId => Guid.Parse("d1f01b7e-3e3a-4f5d-9a3c-1ab2e5eaf8a4");
    public string Username => "test-user";
}