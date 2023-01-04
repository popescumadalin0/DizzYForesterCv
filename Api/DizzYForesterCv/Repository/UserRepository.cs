﻿using Microsoft.EntityFrameworkCore;
using Model.Database;
using Model.Model;

namespace WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUoW _uoW;
        private readonly ITokenRepository _tokenService;
        public UserRepository(IUoW uoW, ITokenRepository tokenService)
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
                    Message = "Can't add the user",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<Response<bool>> UpdateUser(User user)
        {
            try
            {
                _uoW.Users.Update(user);
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
                    Message = "Can't update the user",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<Response<bool>> DeleteUser(int id)
        {
            try
            {
                var user = _uoW.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return new Response<bool>()
                    {
                        Message = "Can't find the user",
                        Status = StatusCodes.Status500InternalServerError,
                        Data = false
                    };
                }
                _uoW.Users.Remove(user);
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
                    Message = "Can't delete the user",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<Response<ResponseLoginModel>> LoginUser(LoginModel user)
        {
            try
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
            catch
            {
                return new Response<ResponseLoginModel>()
                {
                    Message = "Can't verify the user",
                    Status = StatusCodes.Status500InternalServerError,
                };
            }
        }

        public async Task<Response<List<User>>> GetAllUsers()
        {
            try
            {
                var users = await _uoW.Users.ToListAsync();
                return new Response<List<User>>
                {
                    Status = StatusCodes.Status200OK,
                    Data = users
                };
            }
            catch
            {
                return new Response<List<User>>()
                {
                    Message = "Can't get all users",
                    Status = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
