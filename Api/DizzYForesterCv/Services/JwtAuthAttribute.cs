using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Services;
public class JwtAuthAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var services = context.HttpContext.RequestServices;
        var tokenService = (ITokenService)services.GetService(typeof(ITokenService))!;

        var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        try
        {
            if (tokenService.ValidateToken(token))
            {
                return;
            }
        }
        catch (Exception ex)
        {
            context.Result = new JsonResult(ex.Message)
            {
                Value = new
                {
                    Value = "Unauthorized",
                    Message = ex.Message
                }
            };
        }
    }
}
