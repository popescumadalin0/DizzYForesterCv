using Microsoft.AspNetCore.Mvc;
using Model.Model;
using WebAPI.Repository;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsRepository _skillsRepository;
        public SkillsController(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }
        [HttpPost("addSkill")]
        [JwtAuth]
        public async Task<ActionResult<bool>> AddSkill(Skill skill)
        {
            var response = await _skillsRepository.AddSkill(skill);
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
        [HttpDelete("deleteSkill/{id}")]
        [JwtAuth]
        public async Task<ActionResult<bool>> DeleteSkill(int id)
        {
            var response = await _skillsRepository.DeleteSkill(id);
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
        [HttpPut("updateSkill")]
        [JwtAuth]
        public async Task<ActionResult<bool>> UpdateSkill(Skill skill)
        {
            var response = await _skillsRepository.UpdateSkill(skill);
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
        [HttpGet("getAllSkills")]
        [JwtAuth]
        public async Task<ActionResult<List<Skill>>> GetAllSkills()
        {
            var response = await _skillsRepository.GetAllSkills();
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
