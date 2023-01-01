using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Model;
using WebAPI.Repository;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("registerUser")]
        [JwtAuth]
        public async Task<ActionResult<bool>> RegisterUser(User user)
        {
            var response = await _userRepository.AddUser(user);
            return new JsonResult(response.Message)
            {
                Value = new
                {
                    Value = response.Data,
                    Message = response.Message
                },
                StatusCode = response.Status
            };
        }
        [HttpPost("loginUser")]
        public async Task<ActionResult<ResponseLoginModel>> LoginUser(LoginModel user)
        {
            var response = await _userRepository.LoginUser(user);
            return new JsonResult(response.Message)
            {
                Value = new
                {
                    Value = response.Data,
                    Message = response.Message
                },
                StatusCode = response.Status
            };
        }
    }
}
