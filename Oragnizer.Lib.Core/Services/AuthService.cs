using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Organizer.Database.Storage.Repositories.Interfaces;
using Organizer.Database.Storage.Tables;
using Organizer.Lib.Common.Requests;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;
using Organizer.Lib.Core.Providers;
using Organizer.Lib.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Organizer.Lib.Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly JwtSettings _jwtSettings;

    public AuthService(IUserRepository userRepository, ILocalStorageService localStorage, HttpClient client, AuthenticationStateProvider authStateProvider, IOptions<AppSettings> options)
    {
        _userRepository = userRepository;
        _localStorage = localStorage;
        _client = client;
        _authStateProvider = authStateProvider;
        _jwtSettings = options.Value.JwtSettings;
    }

    public async Task<AuthResponse> Login(LoginRequest request)
    {
        try
        {
            var user = await _userRepository.GetAsync(u => u.Login == request.Login);

            if (user == null || !IsPasswordMatching(user, request.Password))
                return new AuthResponse {ErrorMessage = "Błędny login lub hasło"};

            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            await _localStorage.SetItemAsync("authToken", token);
            ((AuthStateProvider) _authStateProvider).NotifyUserAuthentication(user.Id.ToString());
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthResponse {IsAuthSuccessful = true, Token = token};
        }
        catch (Exception ex)
        {
            return new AuthResponse { ErrorMessage = "Invalid Authentication" };
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }

    private static bool IsPasswordMatching(User user, string password)
    {
        var hasher = new PasswordHasher<User>();
        PasswordVerificationResult verificationResult = hasher.VerifyHashedPassword(user, user.Password, password);
        return verificationResult == PasswordVerificationResult.Success;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.ValidIssuer);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private static List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Name ?? ""),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email ?? ""),
        };

        return claims;
    }
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.ExpiryInMinutes)),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }
}