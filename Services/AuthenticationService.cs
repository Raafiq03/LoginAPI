using LoginAPI.Models;
using BCrypt.Net;
using LoginAPI.Entities;
using LoginAPI.Utilities;


namespace LoginAPI.Services
{
    public class AuthenticationService
    {
        private readonly FileUserRepository _repo;

        public AuthenticationService(FileUserRepository repo)
        {
            _repo = repo;
        }

        public void Register(RegisterDto dto)
        {
            var user = _repo.GetAllUsers();
            var newUser = new User
            {
                Id = user.Count + 1,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User",
                Username = dto.Username
            };
            user.Add(newUser);

            _repo.SaveUsers(user);
        }

        public User ValidateCredentials(LoginDto dto)
        {
            var users = _repo.GetAllUsers();

            var user = users.FirstOrDefault(u => u.Email == dto.Email || u.Username == dto.Username);
            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;
            return user;
        }
    }
}




