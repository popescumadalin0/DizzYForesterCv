using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Model.Model;

namespace WebAPI.Repository;

public class TokenRepository : ITokenRepository
{
    private static IConfiguration? _config;

    public TokenRepository(IConfiguration? config)
    {
        _config = config;
    }

    public string GenerateToken(LoginModel user, int durationMin)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config?["Auth0:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.UserName)
        };
        var token = new JwtSecurityToken(_config?["Auth0:Issuer"],
            _config?["Auth0:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(durationMin),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);

    }
    public bool ValidateToken(string token)
    {
        var simplePrinciple = GetPrincipal(token);
        var identity = simplePrinciple?.Identity as ClaimsIdentity;

        return identity is { IsAuthenticated: true };
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
            ValidIssuer = _config?["Auth0:Issuer"],
            ValidAudience = _config?["Auth0:Audience"],
            IssuerSigningKey = symmetricKey,
            ClockSkew = TimeSpan.Zero
        };
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

        return principal;
    }

    public string GenerateName()
    {
        const int length = 10;
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var randomString = new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        var code = Encoding.UTF8.GetBytes(randomString);
        var base64String = Convert.ToBase64String(code);

        return base64String;
    }
}

