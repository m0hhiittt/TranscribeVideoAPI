using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TranscribeVideo.Core.DTOs.Video;
using TranscribeVideo.Core.Interfaces.Services;

namespace TranscribeVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [RequestSizeLimit(2_147_483_648)]
        [RequestFormLimits(MultipartBodyLengthLimit = 2_147_483_648)]
        [DisableRequestSizeLimit]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadVideoDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _videoService.UploadAsync(5, dto);

            return Ok(result);
        }
    }
}
