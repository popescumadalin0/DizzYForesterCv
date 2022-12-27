using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Model.Model;

namespace WebAPI.Services;

public class TokenService : ITokenService
{
    private static IConfiguration? _config;

    public TokenService(IConfiguration? config)
    {
        _config = config;
    }

    public string GenerateToken(LoginModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config?["Auth0:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.UserName),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var token = new JwtSecurityToken(_config?["Auth0:Issuer"],
            _config?["Auth0:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);

    }
    public bool ValidateToken(string token)
    {
        var simplePrinciple = GetPrincipal(token);
        var identity = simplePrinciple?.Identity as ClaimsIdentity;

        return identity is { IsAuthenticated: true };
        // More validate to check whether username exists in system
    }
    public ClaimsPrincipal? GetPrincipal(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (tokenHandler.ReadToken(token) is not JwtSecurityToken)
            return null;

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config?["Auth0:Key"]!));

        var validationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = symmetricKey,
            ClockSkew = TimeSpan.Zero
        };
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

        return principal;
    }
}

