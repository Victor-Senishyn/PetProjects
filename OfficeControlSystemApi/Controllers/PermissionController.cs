using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.Identity;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Interaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OfficeControlSystemApi.Controllers
{
    public class PermissionController : Controller
    {
        private readonly IUserService _userService;

        public PermissionController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("user/")]
        public async Task<IActionResult> CreateAdministrator(
            [FromBody] UserCreationModel user,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.CreateAdministratorUserAsync(user, cancellationToken);

            return Ok("Admin user created successfully.");
        }

        //[HttpGet("{username}")]
        //public IActionResult Get(string username)
        //{
        //    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
        //    var jwt = new JwtSecurityToken(
        //        issuer: AuthOptions.ISSUER,
        //        audience: AuthOptions.AUDIENCE,
        //        claims: claims,
        //        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
        //        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        //    return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        //}
    }
}
