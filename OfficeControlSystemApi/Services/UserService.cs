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

        public async Task CreateUserAsync(
            UserCreationModel userModel,
            CancellationToken cancellationToken = default)
        {
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            var user = new User
            {
                UserName = userModel.Email,
                Email = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            if (!await _userManager.IsInRoleAsync(user, "User"))
                await _userManager.AddToRoleAsync(user, "User");

            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAdministratorAsync(
            UserCreationModel userModel,
            CancellationToken cancellationToken = default)
        {
            if (!await _roleManager.RoleExistsAsync("Administrator"))
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));

            var user = new User
            {
                UserName = userModel.Email,
                Email = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
                throw new Exception($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            if (!await _userManager.IsInRoleAsync(user, "Administrator"))
                await _userManager.AddToRoleAsync(user, "Administrator");

            await _dbContext.SaveChangesAsync();
        }

    }
}
