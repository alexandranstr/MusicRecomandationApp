using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

[Route("api/[controller]")]
[ApiController]
public class RecommendationController : ControllerBase
{
    [HttpGet("{songTitle}")]
    public IActionResult GetRecommendation(string songTitle)
    {
        
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "python"; 
        start.Arguments = $"recommender.py \"{songTitle}\"";
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.CreateNoWindow = true;

        
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                return Ok(new { recommendations = result });
            }
        }
    }
}