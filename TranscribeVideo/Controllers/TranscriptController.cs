using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using TranscribeVideo.Core.Interfaces.Services;

namespace TranscribeVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TranscriptController : ControllerBase
    {
        private readonly ITranscriptionEngine _transcriptionEngine;
        private readonly ITranscriptService _transcriptService;
        private readonly string _storageRoot;

        public TranscriptController(
            ITranscriptService transcriptService,
            ITranscriptionEngine transcriptionEngine,
            IConfiguration configuration)
        {
            _transcriptionEngine = transcriptionEngine;
            _storageRoot = configuration["Storage:RootPath"]
                ?? Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            _transcriptService = transcriptService;
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> GetByVideo(int videoId)
        {
            var result = await _transcriptService.GetByVideoIdAsync(videoId);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("transcribe")]
        public async Task<IActionResult> Transcribe(IFormFile audioFile, string? language)
        {
            if (audioFile == null || audioFile.Length == 0)
                return BadRequest("No audio file was provided.");

            var tempAudioFolder = Path.Combine(_storageRoot, "temp-audio");
            Directory.CreateDirectory(tempAudioFolder);

            var tempAudioName = $"{Guid.NewGuid()}{Path.GetExtension(audioFile.FileName)}";
            var tempAudioPath = Path.Combine(tempAudioFolder, tempAudioName);

            try
            {
                await using (var stream = new FileStream(tempAudioPath, FileMode.Create))
                {
                    await audioFile.CopyToAsync(stream);
                }

                var result = await _transcriptionEngine.TranscribeAsync(tempAudioPath, language);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { message = "Transcription failed.", detail = ex.Message });
            }
            finally
            {
                if (System.IO.File.Exists(tempAudioPath))
                {
                    System.IO.File.Delete(tempAudioPath);
                }
            }
        }
    }
}