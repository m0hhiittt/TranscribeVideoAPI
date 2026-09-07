using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TranscribeVideo.Infrastructure.Services;

namespace TranscribeVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AudioController : ControllerBase
    {
        private readonly string _storageRoot;

        public AudioController(IConfiguration configuration)
        {
            _storageRoot = configuration["Storage:RootPath"]
                ?? Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        }

        /// <summary>
        /// Accepts a video file, extracts its audio track (16kHz mono WAV) via ffmpeg,
        /// and returns the resulting audio file.
        /// </summary>
        [RequestSizeLimit(2_147_483_648)]
        [RequestFormLimits(MultipartBodyLengthLimit = 2_147_483_648)]
        [DisableRequestSizeLimit]
        [HttpPost("extract")]
        public async Task<IActionResult> Extract(IFormFile videoFile)
        {
            if (videoFile == null || videoFile.Length == 0)
                return BadRequest("No video file was provided.");

            var tempVideoFolder = Path.Combine(_storageRoot, "temp-videos");
            Directory.CreateDirectory(tempVideoFolder);

            var tempVideoName = $"{Guid.NewGuid()}{Path.GetExtension(videoFile.FileName)}";
            var tempVideoPath = Path.Combine(tempVideoFolder, tempVideoName);

            try
            {
                await using (var stream = new FileStream(tempVideoPath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                var audioFolder = Path.Combine(_storageRoot, "audio");
                var audioPath = await AudioExtractor.ExtractAudioAsync(tempVideoPath, audioFolder);

                var audioBytes = await System.IO.File.ReadAllBytesAsync(audioPath);
                var audioFileName = Path.GetFileName(audioPath);

                return File(audioBytes, "audio/wav", audioFileName);
            }
            catch (InvalidOperationException ex)
            {
                // Thrown by AudioExtractor when ffmpeg fails or can't start.
                return StatusCode(500, new { message = "Audio extraction failed.", detail = ex.Message });
            }
            finally
            {
                // Clean up the temporary uploaded video; only the extracted audio is kept.
                if (System.IO.File.Exists(tempVideoPath))
                {
                    System.IO.File.Delete(tempVideoPath);
                }
            }
        }
    }
}