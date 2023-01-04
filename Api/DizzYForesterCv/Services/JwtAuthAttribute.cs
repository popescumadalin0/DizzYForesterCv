﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Repository;

namespace WebAPI.Services;
public class JwtAuthAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var services = context.HttpContext.RequestServices;
        var tokenService = (ITokenRepository)services.GetService(typeof(ITokenRepository))!;

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
            context.Result = new JsonResult("You are not authenticated")
            {
                Value = new
                {
                    Value = ex.Message,
                    Message = "You are not authenticated"
                },
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}
