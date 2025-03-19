using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Online_Learning_App_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDto subjectDto)
        {
            if (subjectDto == null || string.IsNullOrWhiteSpace(subjectDto.SubjectName))
            {
                return BadRequest("Invalid subject data.");
            }

            var subjectId = await _subjectService.CreateSubjectAsync(subjectDto);
            return Ok(new { message = subjectId.ToString() });
        }

    }
}
