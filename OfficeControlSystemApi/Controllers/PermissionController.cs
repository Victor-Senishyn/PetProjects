using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;

namespace OfficeControlSystemApi.Controllers
{
    public class PermissionController : Controller
    {
        private readonly AppDbContext _dbContext;

        public PermissionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //[HttpPost("permission"), Authorize(Roles = "Administrator")]
        //public IActionResult GiveAdministratorPermission(string email)
        //{
        //    var user = _dbContext.Users.AsQueryable().Where(user => user.Email == email).FirstOrDefault();
        //    if (user == null)
        //        return BadRequest("User not found");

        //    _dbContext.UserRoles.Add(new User { UserId = user.Id, RoleId = "1" });
        //    _dbContext.SaveChanges();
        //    return Ok("Administrator permission granted successfully");
        //}
    }
}
