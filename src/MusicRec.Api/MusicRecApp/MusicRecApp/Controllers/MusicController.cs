using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicRecApp.Data;
using MusicRecApp.Model;

namespace MusicRecApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MusicController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return Ok(song);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}