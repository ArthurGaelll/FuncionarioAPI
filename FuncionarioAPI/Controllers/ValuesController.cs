using FuncionarioAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuncionarioAPI.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus([FromServices] AppDbContext db)
        {
            // Tenta ver se o banco responde
            var canConnect = db.Database.CanConnect();

            return Ok(new
            {
                status = "Online",
                database = canConnect ? "Connected" : "Disconnected",
                horario = DateTime.Now.ToLongTimeString()
            });
        }
    }
}