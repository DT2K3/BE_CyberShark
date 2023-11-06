using BE_cybershark.Models.BE_cybershark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
    [ApiController]
    [Route("api/database")]
    public class DatabaseController : ControllerBase
    {
        private readonly CyberSharkContext _context;

        public DatabaseController(CyberSharkContext context)
        {
            _context = context;
        }

        [HttpGet("testconnection")]
        public IActionResult TestDatabaseConnection()
        {
            try
            {
                // do simple databasequery 
                var firstUser = _context.Users.FirstOrDefault();

                if (firstUser != null)
                {
                    return Ok("Kết nối cơ sở dữ liệu thành công!");
                }
                else
                {
                    return NotFound("Không có dữ liệu trong cơ sở dữ liệu.");
                }
            }
            catch (DbUpdateException ex)
            {
                // error message
                return BadRequest($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
            }
            catch (Exception ex)
            {
                // other errors
                return BadRequest($"Lỗi không xác định: {ex.Message}");
            }
        }
    }
}