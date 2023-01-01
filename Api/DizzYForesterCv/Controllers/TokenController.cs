using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Model;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("refreshToken")]
        public ActionResult<ResponseLoginModel> RefreshToken(ResponseLoginModel responseLoginModel)
        {
            try
            {
                if (!_tokenService.ValidateToken(responseLoginModel.RefreshToken))
                    return new JsonResult("You are not authenticated")
                    {
                        Value = new
                        {
                            Value = "Unauthorized",
                            Message = "You are not authenticated"
                        },
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                responseLoginModel.Token =
                    _tokenService.GenerateToken(new LoginModel() { UserName = responseLoginModel.UserName }, 1);
                responseLoginModel.RefreshToken =
                    _tokenService.GenerateToken(new LoginModel() { UserName = _tokenService.GenerateName() }, 2);
                return new JsonResult("")
                {
                    Value = new
                    {
                        Value = responseLoginModel,
                        Message = ""
                    },
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message)
                {
                    Value = new
                    {
                        Value = "Unauthorized",
                        Message = ex.Message
                    },
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

        }
    }
}
