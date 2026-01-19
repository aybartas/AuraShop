namespace AuraShop.Shared.Services;

public interface IIdentityService
{
    Guid? UserId { get; }
    string? UserName { get; }
    string? Email { get; }
    IEnumerable<string> Roles { get; }
    bool IsInRole(string role);
    bool IsAuthenticated { get; }
}