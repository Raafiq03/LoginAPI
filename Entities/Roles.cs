using Microsoft.AspNetCore.Identity;

namespace LoginAPI.Entities
{
    public class Roles : IdentityRole
    {
        public string Description { get; set; } = string.Empty;
    }
}

