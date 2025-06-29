using MediatR;

namespace AuraShop.Auth.Features.Login;

public class LoginResponse : IRequest<LoginResponse>
{
    public string AccessToken { get; set; }
}