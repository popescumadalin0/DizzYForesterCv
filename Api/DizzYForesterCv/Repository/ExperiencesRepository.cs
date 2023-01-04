using Microsoft.EntityFrameworkCore;
using Model.Database;
using Model.Model;

namespace WebAPI.Repository
{
    public class ExperiencesRepository : IExperiencesRepository
    {
        private readonly IUoW _uoW;
        public ExperiencesRepository(IUoW uoW)
        {
            _uoW = uoW;
        }
        public async Task<Response<bool>> AddExperience(Experience experience)
        {
            try
            {
                _uoW.Experiences.Add(experience);
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
                    Message = "Can't add the experience",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<Response<bool>> UpdateExperience(Experience experience)
        {
            try
            {
                _uoW.Experiences.Update(experience);
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
                    Message = "Can't update the experience",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<Response<bool>> DeleteExperience(int id)
        {
            try
            {
                var Experience = _uoW.Experiences.FirstOrDefault(x => x.Id == id);
                if (Experience == null)
                {
                    return new Response<bool>()
                    {
                        Message = "Can't find the experience",
                        Status = StatusCodes.Status500InternalServerError,
                        Data = false
                    };
                }
                _uoW.Experiences.Remove(Experience);
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
                    Message = "Can't delete the experience",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<Response<List<Experience>>> GetAllExperiences()
        {
            try
            {
                var experiences = await _uoW.Experiences.ToListAsync();
                return new Response<List<Experience>>
                {
                    Status = StatusCodes.Status200OK,
                    Data = experiences
                };
            }
            catch
            {
                return new Response<List<Experience>>()
                {
                    Message = "Can't get all experiences",
                    Status = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
