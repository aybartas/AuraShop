using AuraShop.Auth.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AuraShop.Auth.Services
{
    public class KeycloakService(HttpClient http, IOptions<KeycloakConfig> options, KeycloakEndpoints endpoints)
    {
        private readonly KeycloakConfig _options = options.Value;

        private static async Task<T> ParseResponseAsync<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Keycloak API Error: {response.StatusCode} - {content}");
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<TokenResponse> LoginAsync(string email, string password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", _options.ClientId),
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", email),
            new KeyValuePair<string, string>("password", password),
            new KeyValuePair<string, string>("scope", "openid")  

        });

            var response = await http.PostAsync(endpoints.TokenEndpoint, content);

            return await ParseResponseAsync<TokenResponse>(response);
        }

        public async Task<string> GetAdminTokenAsync()
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _options.AdminClientId),       
                new KeyValuePair<string, string>("client_secret", _options.AdminClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")   
            });

            var response = await http.PostAsync(endpoints.AdminTokenEndpoint, content);

            var tokenResponse = await ParseResponseAsync<TokenResponse>(response);

            return tokenResponse.AccessToken;
        }

        public async Task<string> RegisterUserAsync(string email,string password)
        {
            var token = await GetAdminTokenAsync();

            var userRequest = new UserRegistrationRequest
            {
                Username = email,
                Email = email,
                Enabled = true,
                Credentials =
                [
                    new Credential
                {
                    Type = "password",
                    Value = password,
                    Temporary = false
                }
                ]
            };

            var request = new HttpRequestMessage(HttpMethod.Post, endpoints.UsersEndpoint());
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(JsonSerializer.Serialize(userRequest), Encoding.UTF8, "application/json");

            var response = await http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var location = response.Headers.Location;
            if (location == null)
                throw new InvalidOperationException("User creation location header missing");

            var userId = location.Segments.Last();

            await AssignRealmRoleToUserAsync(userId, "user");

            return userId;
        }

        private async Task<JsonElement> GetRealmRoleAsync(string roleName, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoints.RolesEndpoint(roleName));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(json);

            return doc.RootElement;
        }

        public async Task<User> GetUserInfoAsync(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoints.UserInfoEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await http.SendAsync(request);

            return await ParseResponseAsync<User>(response);
        }

        private async Task AssignRealmRoleToUserAsync(string userId, string roleName)
        {
            var token = await GetAdminTokenAsync();

            var role = await GetRealmRoleAsync(roleName, token);

            var rolesPayload = new[]
            {
            new
            {
                id = role.GetProperty("id").GetString(),
                name = role.GetProperty("name").GetString()
            }
        };

            var request = new HttpRequestMessage(HttpMethod.Post, endpoints.AssignRoleToUserEndpoint(userId));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(JsonSerializer.Serialize(rolesPayload), Encoding.UTF8, "application/json");

            var response = await http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }

}
