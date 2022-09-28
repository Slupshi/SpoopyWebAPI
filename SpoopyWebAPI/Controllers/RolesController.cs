using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using SpoopyWebAPI.Models;

namespace SpoopyWebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class RolesController : Controller
    {
        private IConfiguration _configuration;
        public RolesController(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }

        [HttpGet]
        [Route("roles")]
        public async Task<IEnumerable<SpoopyRole>> GetRoles()
        {
            List<SpoopyRole> roles = new List<SpoopyRole>();
            await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("SpoopyDB"));
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT * FROM roles", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    roles.Add(new SpoopyRole
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        MemberCount = reader.GetInt32(2),
                        CreatedAt = reader.GetDateTime(3),
                    });
                }
            }
            await conn.CloseAsync();
            return roles;
        }

        [HttpGet]
        [Route("roles/{id}")]
        public async Task<SpoopyRole?> GetRoleById(long id)
        {
            SpoopyRole? role = null;
            await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("SpoopyDB"));
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT * FROM roles WHERE ID = $1", conn))
            {
                cmd.Parameters.AddWithValue(id);
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    role = new SpoopyRole()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        MemberCount = reader.GetInt32(2),
                        CreatedAt = reader.GetDateTime(3),
                    };
                }
            }
            await conn.CloseAsync();
            return role;            
        }

        [HttpGet]
        [Route("roles/name/{name}")]
        public async Task<SpoopyRole?> GetRoleByName(string name)
        {
            SpoopyRole? role = null;
            await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("SpoopyDB"));
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT * FROM roles WHERE NAME = $1", conn))
            {
                cmd.Parameters.AddWithValue(name);
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    role = new SpoopyRole()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        MemberCount = reader.GetInt32(2),
                        CreatedAt = reader.GetDateTime(3),
                    };
                }
            }
            await conn.CloseAsync();
            return role;
        }

        [HttpPut]
        [Route("roles/{id}")]
        public async Task PutRole(SpoopyRole role, long id)
        {
            await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("SpoopyDB"));
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("UPDATE roles SET name = $1, membercount = $2, createdat = $3 WHERE ID = $4", conn))
            {
                cmd.Parameters.AddWithValue(role.Name);
                cmd.Parameters.AddWithValue(role.MemberCount);
                cmd.Parameters.AddWithValue(role.CreatedAt);
                cmd.Parameters.AddWithValue(id);
                await cmd.ExecuteNonQueryAsync();
            }

            await conn.CloseAsync();
        }

        [HttpPost]
        [Route("roles")]
        public async Task PostRole(SpoopyRole role)
        {
            await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("SpoopyDB"));
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("INSERT INTO roles (name, membercount, createdat) VALUES ($1, $2, $3)", conn))
            {
                cmd.Parameters.AddWithValue(role.Name);
                cmd.Parameters.AddWithValue(role.MemberCount);
                cmd.Parameters.AddWithValue(role.CreatedAt);
                await cmd.ExecuteNonQueryAsync();
            }

            await conn.CloseAsync();
        }
    }
}
