using Microsoft.AspNetCore.Mvc;
using Npgsql;
using SpoopyWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpoopyWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class LogsController : Controller
    {
        [HttpGet]
        [Route("logs")]
        public async Task<IEnumerable<SpoopyLogs>> Get()
        {
            List<SpoopyLogs> logs = new List<SpoopyLogs>();
            await using var conn = new NpgsqlConnection(Properties.ConnectionString);
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT * FROM logs", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    logs.Add(new SpoopyLogs
                    {
                        Id = reader.GetInt32(0),
                        Message = reader.GetString(1),
                        IsError = reader.GetBoolean(2),
                        Time = reader.GetDateTime(3),
                    });
                }
            }
            await conn.CloseAsync();
            return logs;
        }

        [HttpGet]
        [Route("logs/{id}")]
        public SpoopyLogs Get(int id)
        {
            return new SpoopyLogs { Id = id };
        }

        
        [HttpPost]
        [Route("logs")]
        public async Task PostLogs([FromBody] SpoopyLogs logs)
        {
            await using var conn = new NpgsqlConnection(Properties.ConnectionString);
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("INSERT INTO logs (time, message, iserror) VALUES ($1, $2, $3)", conn))
            {
                cmd.Parameters.AddWithValue(logs.Time);
                cmd.Parameters.AddWithValue(logs.Message);
                cmd.Parameters.AddWithValue(logs.IsError);
                await cmd.ExecuteNonQueryAsync();
            }

            await conn.CloseAsync();
        }

        [HttpPut]
        [Route("logs/{id}")]
        public void Put(int id, [FromBody] SpoopyLogs logs)
        {
        }

        [HttpDelete]
        [Route("logs/{id}")]
        public void Delete(int id)
        {
        }
    }
}
