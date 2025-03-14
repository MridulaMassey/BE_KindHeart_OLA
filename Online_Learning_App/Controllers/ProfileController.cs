using System.Data;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;

namespace Online_Learning_App_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

      

        [HttpPost("getProfile")]
        public async Task<IActionResult> getProfile([FromBody] ProfileDTO model)
        {
            var profile = _userService.GetProfileAsync(model.userName);
            if (profile == null) 
            {
                return Unauthorized(new { message = "Profile not found" });
            }

            return Ok(new { message = "Profile retrieved succesfully", ProfileDetails= profile });

        }


    }
}
