using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_App_Presentation.Controllers
{
    [Route("api/classgroups")]
    [ApiController]
    public class ClassGroupController : ControllerBase
    {
        private readonly IClassGroupService _classGroupService;

        public ClassGroupController(IClassGroupService classGroupService)
        {
            _classGroupService = classGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassGroup([FromBody] ClassGroupCreateDto classGroupDto)
        {
            if (classGroupDto == null || string.IsNullOrWhiteSpace(classGroupDto.ClassName))
            {
                return BadRequest("Invalid class group data.");
            }

            try
            {
                var classGroup = await _classGroupService.CreateClassGroupAsync(classGroupDto);
                return Ok(classGroup);
                // return CreatedAtAction(nameof(classGroup), new { id = classGroup.ClassGroupId }, classGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClassGroups()
        {
            var classGroups = await _classGroupService.GetAllClassGroupsAsync();

            if (classGroups == null || !classGroups.Any())
                return NotFound();

            return Ok(classGroups);
        }


        /*    [httpget("{id}")*/
        //]
        //public async task<iactionresult> getclassgroup(guid id)
        //{
        //   // var classgroup = await _classgroupservice.getclassgroupbyidasync(id);
        //    var classgroup = null;
        //    if (classgroup == null)
        //        return notfound();

        //    return ok(classgroup);
        //}

    }
}
