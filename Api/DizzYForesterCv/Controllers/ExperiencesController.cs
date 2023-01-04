using Microsoft.AspNetCore.Mvc;
using Model.Model;
using WebAPI.Repository;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly IExperiencesRepository _experiencesRepository;
        public ExperiencesController(IExperiencesRepository experiencesRepository)
        {
            _experiencesRepository = experiencesRepository;
        }
        [HttpPost("addExperience")]
        [JwtAuth]
        public async Task<ActionResult<bool>> AddExperience(Experience experience)
        {
            var response = await _experiencesRepository.AddExperience(experience);
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
        [HttpDelete("deleteExperience/{id}")]
        [JwtAuth]
        public async Task<ActionResult<bool>> DeleteExperience(int id)
        {
            var response = await _experiencesRepository.DeleteExperience(id);
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
        [HttpPut("updateExperience")]
        [JwtAuth]
        public async Task<ActionResult<bool>> UpdateExperience(Experience experience)
        {
            var response = await _experiencesRepository.UpdateExperience(experience);
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
        [HttpGet("getAllExperiences")]
        [JwtAuth]
        public async Task<ActionResult<List<Experience>>> GetAllExperiences()
        {
            var response = await _experiencesRepository.GetAllExperiences();
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
