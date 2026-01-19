using Microsoft.AspNetCore.Authorization;

namespace AuraShop.Shared.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AdminOnlyAttribute : AuthorizeAttribute
{
    public AdminOnlyAttribute() : base(Policies.AdminOnly)
    {
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class CustomerOnlyAttribute : AuthorizeAttribute
{
    public CustomerOnlyAttribute() : base(Policies.CustomerOnly)
    {
    }
}
