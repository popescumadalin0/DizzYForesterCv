using Model.Model;
using System.Security.Claims;

namespace WebAPI.Repository
{
    public interface ITokenRepository
    {
        public string GenerateToken(LoginModel user, int durationMin);
        public bool ValidateToken(string token);
        public ClaimsPrincipal? GetPrincipal(string token);
        public string GenerateName();
    }
}
