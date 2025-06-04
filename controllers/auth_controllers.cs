using Microsoft.AspNetCore.Mvc;
using TODOAPI.Data;
using TODOAPI.Models;

namespace TODOAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // ini jadi 'api/auth'
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (existingUser == null)
                return Unauthorized("Username atau password salah");

            return Ok("Login berhasil");
        }
    }
}
