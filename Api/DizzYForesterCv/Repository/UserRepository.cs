using System.Net;
using Microsoft.EntityFrameworkCore;
using Model.Database;
using Model.Model;
using WebAPI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUoW _uoW;
        private readonly ITokenService _tokenService;
        public UserRepository(IUoW uoW, ITokenService tokenService)
        {
            _uoW = uoW;
            _tokenService = tokenService;
        }
        public async Task<Response<bool>> AddUser(User user)
        {
            try
            {
                _uoW.Users.Add(user);
                await _uoW.SaveChangesAsync();
                return new Response<bool>()
                {
                    Status = StatusCodes.Status200OK,
                    Data = true
                };
            }
            catch
            {
                return new Response<bool>()
                {
                    Message = "Something went wrong",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public Task<Response<bool>> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ResponseLoginModel>> LoginUser(LoginModel user)
        {
            var checkUserName = await _uoW.Users.Where(u => u.UserName == user.UserName).ToListAsync();
            if (checkUserName.Count == 0)
            {
                return new Response<ResponseLoginModel>
                {
                    Message = "Username don't exists",
                    Status = StatusCodes.Status409Conflict
                };
            }
            var checkPassword = checkUserName.FirstOrDefault(u => u.Password == user.Password);
            if (checkPassword == null)
            {
                return new Response<ResponseLoginModel>
                {
                    Message = "Password is incorrect",
                    Status = StatusCodes.Status409Conflict
                };
            }

            var refreshTokenName = _tokenService.GenerateName();
            var logonUser = new ResponseLoginModel
            {
                UserName = checkPassword.UserName,
                Token = _tokenService.GenerateToken(user, 60),
                RefreshToken = _tokenService.GenerateToken(new LoginModel() { UserName = refreshTokenName }, 7200)
            };

            return new Response<ResponseLoginModel>
            {
                Status = StatusCodes.Status200OK,
                Data = logonUser
            };
        }
    }
}
