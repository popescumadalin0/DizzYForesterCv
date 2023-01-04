using Microsoft.AspNetCore.Mvc;
using Model.Model;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenRepository _tokenService;

        public TokenController(ITokenRepository tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("refreshToken")]
        public ActionResult<ResponseLoginModel> RefreshToken(ResponseLoginModel responseLoginModel)
        {
            try
            {
                if (!_tokenService.ValidateToken(responseLoginModel.RefreshToken))
                    return new JsonResult("Your session expired")
                    {
                        Value = new
                        {
                            Value = "",
                            Message = "Your session expired"
                        },
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                responseLoginModel.Token =
                    _tokenService.GenerateToken(new LoginModel() { UserName = responseLoginModel.UserName }, 60);
                responseLoginModel.RefreshToken =
                    _tokenService.GenerateToken(new LoginModel() { UserName = _tokenService.GenerateName() }, 7200);
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
                return new JsonResult("Your session expired")
                {
                    Value = new
                    {
                        Value = ex.Message,
                        Message = "Your session expired"
                    },
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

        }
    }
}
