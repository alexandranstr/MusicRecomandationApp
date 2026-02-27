using Microsoft.AspNetCore.Mvc;
using MusicRecApp.Model;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context = new AppDbContext();

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        var exists = _context.Users.Any(u => u.Username == user.Username);

        if (exists)
        {
            return Conflict(new { message = "User already exists!" });
        }

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new { message = "User created!", userId = user.Id });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        var found = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
        if (found == null) return Unauthorized();
        return Ok(found); 
    }
}