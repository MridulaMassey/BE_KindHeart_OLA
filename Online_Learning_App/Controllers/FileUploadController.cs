
using Microsoft.AspNetCore.Mvc;
using Online_Learning_APP.Application.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GoogleDriveUploader.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class FileUploadController : ControllerBase
    {
        private readonly FileUploadService _fileUploadService;

        public FileUploadController(FileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadRequest request)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await request.File.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                string downloadUrl = await _fileUploadService.UploadFileAsync(fileBytes, request.File.FileName);
                return Ok(new { downloadUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    public class FileUploadRequest
    {
        public IFormFile File { get; set; }
    }
}
