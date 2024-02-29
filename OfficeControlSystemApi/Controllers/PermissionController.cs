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

        [HttpPost("administrator/")]
        public async Task<IActionResult> CreateUser(
            [FromBody] UserCreationModel user,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _userService.CreateAdministratorAsync(user, cancellationToken);
                return Ok("Adminstrator created successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Wrong data");
            }
        }

        [HttpPost("user/")]
        public async Task<IActionResult> CreateAdministrator(
            [FromBody] UserCreationModel user,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _userService.CreateUserAsync(user, cancellationToken);
                return Ok("User created successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Wrong data");
            }
        }
    }
}
