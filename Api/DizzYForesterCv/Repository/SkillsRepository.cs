using Microsoft.EntityFrameworkCore;
using Model.Database;
using Model.Model;

namespace WebAPI.Repository
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly IUoW _uoW;
        public SkillsRepository(IUoW uoW)
        {
            _uoW = uoW;
        }
        public async Task<Response<bool>> AddSkill(Skill skill)
        {
            try
            {
                _uoW.Skills.Add(skill);
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
                    Message = "Can't add the skill",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<Response<bool>> UpdateSkill(Skill skill)
        {
            try
            {
                _uoW.Skills.Update(skill);
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
                    Message = "Can't update the skill",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<Response<bool>> DeleteSkill(int id)
        {
            try
            {
                var skill = _uoW.Skills.FirstOrDefault(x => x.Id == id);
                if (skill == null)
                {
                    return new Response<bool>()
                    {
                        Message = "Can't find the skill",
                        Status = StatusCodes.Status500InternalServerError,
                        Data = false
                    };
                }
                _uoW.Skills.Remove(skill);
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
                    Message = "Can't delete the skill",
                    Status = StatusCodes.Status500InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<Response<List<Skill>>> GetAllSkills()
        {
            try
            {
                var skills = await _uoW.Skills.ToListAsync();
                return new Response<List<Skill>>
                {
                    Status = StatusCodes.Status200OK,
                    Data = skills
                };
            }
            catch
            {
                return new Response<List<Skill>>()
                {
                    Message = "Can't get all skills",
                    Status = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
