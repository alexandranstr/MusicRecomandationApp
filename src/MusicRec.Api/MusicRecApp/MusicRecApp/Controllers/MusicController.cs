using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicRecApp.Model;

namespace MusicRecApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        [HttpGet("test-songs")]
        public IActionResult GetSongs()
        {
            var songs = new List<Song>
            {
                new Song { Id = 1, Title = "Blinding Lights", Artist = "The Weeknd", Genre = "Pop", Tempo = 171, Energy = 0.8 },
                new Song { Id = 2, Title = "Stairway to Heaven", Artist = "Led Zeppelin", Genre = "Rock", Tempo = 82, Energy = 0.5 }
            };

            return Ok(songs);
        }
    }
}
