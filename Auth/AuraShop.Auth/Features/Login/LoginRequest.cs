using AuraShop.Shared;
using MediatR;

namespace AuraShop.Auth.Features.Login;

public class LoginRequest : IRequest<ServiceResult<LoginResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}