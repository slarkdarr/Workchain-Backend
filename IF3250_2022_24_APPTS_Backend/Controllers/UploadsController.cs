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
            return Ok(new { profile_picture = "https://img.okezone.com/content/2022/02/07/33/2543401/jennie-blackpink-dihina-tagar-leavejenniealone-trending-kjFBq7pSGe.jpg" });
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
            return Ok(new { requirement_link = "https://drive.google.com/file/d/1EgfOv3xmOdDn8YUOrD5MOpoNJokGVuTh/view?usp=sharing" });
        }
    }
}
