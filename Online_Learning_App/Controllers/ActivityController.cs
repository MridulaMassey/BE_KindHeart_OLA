using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_App_Presentation.Controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost]
        public async Task<ActionResult<ActivityDto>> CreateActivity([FromBody] CreateActivityDto createActivityDto)
        {
            var activityDto = await _activityService.CreateActivityAsync(createActivityDto);
            return CreatedAtAction(nameof(GetActivity), new { id = activityDto.ActivityId }, activityDto);
        }

        [HttpGet("{id}")]
        public ActionResult<ActivityDto> GetActivity(Guid id)
        {
            //Logic to get activity.
            return null;
        }
    }
}
