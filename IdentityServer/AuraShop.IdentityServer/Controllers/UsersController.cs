using System.Threading.Tasks;
using AuraShop.IdentityServer.Dtos;
using AuraShop.IdentityServer.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.IdentityServer.Controllers
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var values = new ApplicationUser
            {
                Id = null,
                UserName = userRegisterDto.UserName,
                NormalizedUserName = null,
                Email = userRegisterDto.Email,
                NormalizedEmail = null,
                EmailConfirmed = false,
                PasswordHash = null,
                SecurityStamp = null,
                ConcurrencyStamp = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname
            };

            var result = await _userManager.CreateAsync(values,userRegisterDto.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
