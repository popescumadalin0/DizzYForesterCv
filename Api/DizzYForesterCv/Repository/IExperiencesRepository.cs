using Model.Model;

namespace WebAPI.Repository
{
    public interface IExperiencesRepository
    {
        public Task<Response<bool>> AddExperience(Experience experience);
        public Task<Response<bool>> UpdateExperience(Experience experience);
        public Task<Response<bool>> DeleteExperience(int id);
        public Task<Response<List<Experience>>> GetAllExperiences();
    }
}
