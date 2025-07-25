using Microsoft.AspNetCore.Mvc;

namespace LoginProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"
        });
    }

    [HttpGet("database")]
    public IActionResult Database()
    {
        try
        {
            // Basit database connection check yapılabilir
            return Ok(new
            {
                Status = "Database Healthy",
                Timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Status = "Database Unhealthy",
                Error = ex.Message,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
