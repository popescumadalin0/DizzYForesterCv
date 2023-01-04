﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpDelete("deleteUser/{id}")]
        [JwtAuth]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var response = await _userRepository.DeleteUser(id);
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
        [HttpPut("updateUser")]
        [JwtAuth]
        public async Task<ActionResult<bool>> UpdateUser(User user)
        {
            var response = await _userRepository.UpdateUser(user);
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
        [HttpGet("getAllUsers")]
        [JwtAuth]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var response = await _userRepository.GetAllUsers();
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
