using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranscribeVideo.Core.Interfaces.Services;

namespace TranscribeVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet("{transcriptId}")]
        public async Task<IActionResult> GetByTranscript(int transcriptId)
        {
            var result = await _summaryService.GetByTranscriptIdAsync(transcriptId);

            return result == null ? NotFound() : Ok(result);
        }
    }
}
