using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerChat.ApplicationDbContext;
using System.Net.Http;

namespace ServerChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetRegisteredUsers()
        {
            try
            {
                var users = await _context.Users
                    .Select(u => u.Username)
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
