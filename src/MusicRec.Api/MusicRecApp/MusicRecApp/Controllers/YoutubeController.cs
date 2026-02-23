using Microsoft.AspNetCore.Mvc;
using MusicRecApp.Services;

namespace MusicRecApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeController : ControllerBase
    {
        private readonly YouTubeService _youtubeService;

        public YouTubeController(YouTubeService youtubeService)
        {
            _youtubeService = youtubeService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var result = await _youtubeService.SearchTrack(query);
            if (result == null) return NotFound("No results found");
            return Ok(result);
        }
    }
}