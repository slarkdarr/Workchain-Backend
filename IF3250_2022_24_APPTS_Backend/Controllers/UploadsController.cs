using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.Upload;
using IF3250_2022_24_APPTS_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IF3250_2022_24_APPTS_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        private IGoogleDriveService _googleDriveService;
        private readonly AppSettings _appSettings;

        public UploadController(
            IGoogleDriveService googleDriveService,
            IOptions<AppSettings> appSettings)
        {
            _googleDriveService = googleDriveService;
            _appSettings = appSettings.Value;
        }

        /// <summary>Upload User Profile Picture</summary>
        /// <returns>profile_picture</returns>
        /// <remarks>
        /// Requires Bearer Token in Header
        /// </remarks>
        [HttpPost("picture")]
        public async Task<IActionResult> UploadPicture([FromForm] UploadPictureRequest model)
        {
            var memorystream = new MemoryStream();
            await model.picture.CopyToAsync(memorystream);
            var response = await _googleDriveService.UploadFile(memorystream, model.picture.FileName, model.picture.ContentType, "root");
            return Ok(response);
        }

        /// <summary>Upload User CV</summary>
        /// <returns>requirement_link</returns>
        /// <remarks>
        /// Requires Bearer Token in Header
        /// </remarks>
        [HttpPost("file")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest model)
        {
            var memorystream = new MemoryStream();
            await model.file.CopyToAsync(memorystream);
            var response = await _googleDriveService.UploadFile(memorystream, model.file.FileName, model.file.ContentType, "root");
            return Ok(response);
        }
    }
}
