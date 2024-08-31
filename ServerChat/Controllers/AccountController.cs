using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerChat.ApplicationDbContext;
using ServerChat.Model;
using System.Security.Claims;

namespace ServerChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            if (await _context.Users.AnyAsync(u => u.Username == userModel.Username))
            {
                return BadRequest("User already exists.");
            }

            var user = new User
            {
                Username = userModel.Username,
                Password = userModel.Password // Hash password in production
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel userModel)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userModel.Username && u.Password == userModel.Password);

            if (user != null)
            {
                // Create a simple authentication cookie
                var claims = new[] { new Claim(ClaimTypes.Name, userModel.Username) };
                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("Cookies", principal);

                return Ok("Login successful.");
            }

            return Unauthorized("Invalid username or password.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Ok("Logout successful.");
        }
    }
}
