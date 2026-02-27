using LoginAPI.Models;
using LoginAPI.Data;
using BCrypt.Net;


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
                Role = "User"
            };
            user.Add(newUser);

            _repo.SaveUsers(user);
        }

        public User ValidateCredentials(LoginDto dto)
        {
            var users = _repo.GetAllUsers();

            var user = users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;
            return user;
        }
    }
}
