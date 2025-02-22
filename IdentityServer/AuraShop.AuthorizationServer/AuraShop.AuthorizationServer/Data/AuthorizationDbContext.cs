using AuraShop.AuthorizationServer.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.AuthorizationServer.Data
{
    public class AuthorizationDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {
            
        }

    }
}
