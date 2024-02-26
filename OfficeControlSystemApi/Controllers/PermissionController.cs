using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OfficeControlSystemApi.Controllers
{
    public class PermissionController : Controller
    {
        private readonly AppDbContext _dbContext;

        public PermissionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }
    }
}
