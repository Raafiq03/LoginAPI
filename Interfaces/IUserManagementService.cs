using LoginAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginAPI.Interfaces
{
    public interface IUserManagementService
    {
        Task<IActionResult> Register(RegisterDto dto);


    }
}
