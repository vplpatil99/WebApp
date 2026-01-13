using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OptimalRXBE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OptimalRXBE.DTOs;

namespace OptimalRXBE.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            if (req == null || string.IsNullOrEmpty(req.Username) || string.IsNullOrEmpty(req.Password))
                return BadRequest("Username and Password are required");

            // 🔐 Validate user from Accessibility table
            var user = await _db.Accessibilities
                .FirstOrDefaultAsync(x =>
                    x.Username == req.Username &&
                    x.Passwd == req.Password); // legacy password match

            if (user == null)
                return Unauthorized("Invalid username or password");

            // 🔑 JWT Key
            var jwtKey = _config["Jwt:Key"]
                ?? throw new Exception("JWT Key missing in appsettings.json");

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            // 🧾 Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserType", user.UserType ?? ""),
                new Claim("BranchId", user.Branchid?.ToString() ?? "")
            };

            // 🔐 Module permissions
            AddClaim(claims, "Master", user.Master);
            AddClaim(claims, "Stock", user.Stock);
            AddClaim(claims, "Orders", user.Orders);
            AddClaim(claims, "Reports", user.Reports);
            AddClaim(claims, "Invoice", user.Invoice);
            AddClaim(claims, "Measurements", user.Measurements);
            AddClaim(claims, "BatchPro", user.BatchPro);
            AddClaim(claims, "Breakage", user.Breakage);
            AddClaim(claims, "Payment", user.Payment);
            AddClaim(claims, "Lens", user.Lens);

            // 🎟️ Create JWT Token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                username = user.Username,
                userType = user.UserType,
                branchId = user.Branchid
            });
        }

        private void AddClaim(List<Claim> claims, string name, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value) && value == "Y")
            {
                claims.Add(new Claim(name, "true"));
            }
        }
    }
}
