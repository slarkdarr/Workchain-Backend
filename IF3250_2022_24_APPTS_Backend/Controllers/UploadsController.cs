using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.Upload;
using IF3250_2022_24_APPTS_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Horizon.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        //private IGoogleDriveService _googleDriveService;

        public UploadController(
            //IGoogleDriveService googleDriveService,
        )
        {
            //_googleDriveService = googleDriveService;
        }

        /// <summary>Upload User Profile Picture</summary>
        /// <returns>profile_picture</returns>
        /// <remarks>
        /// Requires Bearer Token in Header
        /// </remarks>
        [HttpPost("picture")]
        public async Task<IActionResult> UploadPicture([FromForm] UploadPictureRequest model)
        {
            // TODO : Azure Blob
            return Ok(new { profile_picture = "link" });
        }

        /// <summary>Upload User CV</summary>
        /// <returns>requirement_link</returns>
        /// <remarks>
        /// Requires Bearer Token in Header
        /// </remarks>
        [HttpPost("file")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest model)
        {
            // TODO : Azure Blob
            return Ok(new { requirement_link = "link" });
        }
    }
}
