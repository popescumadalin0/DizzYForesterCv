using Model.Model;

namespace WebAPI.Repository
{
    public interface ISkillsRepository
    {
        public Task<Response<bool>> AddSkill(Skill skill);
        public Task<Response<bool>> UpdateSkill(Skill skill);
        public Task<Response<bool>> DeleteSkill(int id);
        public Task<Response<List<Skill>>> GetAllSkills();
    }
}
