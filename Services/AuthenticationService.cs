using LoginAPI.Models;
using BCrypt.Net;
using LoginAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace LoginAPI.Services
{
    public class AuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Register(RegisterDto dto)
        {
            
            var user = new User
            {
                Email = dto.Email,
                UserName = dto.Username,
                Role = "User"
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }
            user.Role = "User";
            await _userManager.UpdateAsync(user);

            return new OkObjectResult("User registered successfully");
        }

        public async Task<User> ValidateCredentials(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                user = await _userManager.FindByNameAsync(dto.Username);

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return null;
            return user;
        }
    }
}




