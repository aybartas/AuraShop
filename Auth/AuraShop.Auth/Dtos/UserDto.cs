namespace AuraShop.Auth.Dtos;

public class UserDto
{
    public string Email { get; set; }
    public string Username { get; set; }
    public List<string> Roles { get; set; }
}