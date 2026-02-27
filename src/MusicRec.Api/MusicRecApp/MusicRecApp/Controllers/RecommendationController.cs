using Microsoft.AspNetCore.Mvc;
using MusicRecApp.Model;
using MusicRecApp.Services;
using System.Text.Json;

namespace MusicRecApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly YouTubeMusicService _youtubeService;

        public RecommendationController(YouTubeMusicService youtubeService)
        {
            _httpClient = new HttpClient();
            _youtubeService = youtubeService;
        }

        [HttpGet("get-discovery/{songTitle}")]
        public async Task<IActionResult> GetDiscovery(string songTitle)
        {
            var finalResults = new List<SongDto>();
            string cleanedTitle = songTitle.Split('(')[0].Split("ft.")[0].Split("feat.")[0].Trim();

            try
            {
                var pythonResponse = await _httpClient.GetAsync($"http://localhost:5000/recommend?title={Uri.EscapeDataString(cleanedTitle)}");
               
                if (pythonResponse.IsSuccessStatusCode)
                {
                    var content = await pythonResponse.Content.ReadAsStringAsync();
                    var dbRecommendations = JsonSerializer.Deserialize<List<SongDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (dbRecommendations != null && dbRecommendations.Count > 0)
                    {
                        foreach (var rec in dbRecommendations)
                        {
                            string query = $"{rec.Artist} {rec.Title}";
                            var video = await _youtubeService.SearchSingleVideoAsync(query);

                            if (video != null)
                            {
                                video.Title = rec.Title;
                                video.Artist = rec.Artist;
                                finalResults.Add(video);
                            }
                        }
                    }
                }
                if (finalResults.Count == 0)
                {
                    finalResults = await _youtubeService.SearchRelatedVideosAsync(songTitle);
                }

                return Ok(finalResults);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepție: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}