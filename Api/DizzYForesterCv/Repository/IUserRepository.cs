using Model.Model;

namespace WebAPI.Repository
{
    public interface IUserRepository
    {
        public Task<Response<bool>> AddUser(User user);
        public Task<Response<bool>> UpdateUser(User user);
        public Task<Response<bool>> DeleteUser(int id);
        public Task<Response<ResponseLoginModel>> LoginUser(LoginModel user);
        public Task<Response<List<User>>> GetAllUsers();
    }
}
