
using Microsoft.AspNetCore.Identity;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace AuthenticationApp.Application.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> RegisterUserAsync(string username, string email, string password, string roleName)
        {
            // Check if the role exists
            //var roleExists = await _roleManager.RoleExistsAsync(roleName);
            //if (!roleExists)
            //{
            //    return "Role does not exist!";
            //}
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return "Role does not exist!";
            }

            // Create a new user
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };
           //  password = _userManager.PasswordHasher.HashPassword(user, "India@123"); // Correct hashing
            //   await _userManager.UpdateAsync(user);
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return "User creation failed!";
            }

            user.RoleId = role.Id;  // Ensure your ApplicationUser entity has a RoleId property
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password); // Correct hashing
            await _userManager.UpdateAsync(user);
            // Assign role to user
            await _userManager.AddToRoleAsync(user, roleName);
              //user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, "India@123"); // Correct hashing
              //await _userManager.UpdateAsync(user);

            return "User registered successfully!";
        }
    }
}
