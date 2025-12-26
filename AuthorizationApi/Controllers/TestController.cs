using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AuthorizationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetDbConnection()
        {
            var connectionString = "Server=localhost;Database=master;User Id=sa;Password=Strong_Password123!;TrustServerCertificate=True;";

            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            return Ok("Succ");
        }
    }
}
