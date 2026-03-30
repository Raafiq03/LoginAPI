using LoginAPI.Entities;
using LoginAPI.Interfaces;
using LoginAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<User> _userManager;

        public UserManagementService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
                Role = "User"
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }
            return new OkObjectResult(new { message = "User registered successfully" });
        }
    }
}
