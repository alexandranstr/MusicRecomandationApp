using Microsoft.AspNetCore.Mvc;
using MusicRecApp.Model;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly AppDbContext _context;
    public FavoritesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("add")]
    public IActionResult AddFavorite([FromBody] FavoriteSong fav)
    {
        try
        {
            _context.Favorites.Add(fav);
            _context.SaveChanges();
            return Ok(new { message = "Added to favorites!" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userId}")]
    public IActionResult GetFavorites(int userId)
    {
        var favs = _context.Favorites
                           .Where(f => f.UserId == userId)
                           .ToList();
        return Ok(favs);
    }
}