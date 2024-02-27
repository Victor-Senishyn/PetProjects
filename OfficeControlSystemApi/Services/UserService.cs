using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.Identity;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;

        public UserService(
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager,
            AppDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task CreateAdministratorUserAsync(
            UserCreationModel userModel, 
            CancellationToken cancellationToken = default)
        {
            if (!await _roleManager.RoleExistsAsync("Administrator"))
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));

            var user = _dbContext.Users.Where( u => u.Email == userModel.Email).SingleOrDefault();
            
            if (user == null)
                throw new ArgumentException();

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "Administrator");

            await _dbContext.SaveChangesAsync();
        }
    }
}
