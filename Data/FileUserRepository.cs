using System.Text.Json;
using LoginAPI.Models;

namespace LoginAPI.Data;


public class FileUserRepository
{
	private readonly string _filePath;

    public FileUserRepository(IWebHostEnvironment env)
	{
		_filePath = Path.Combine(env.ContentRootPath, "users.json");
	}

	public List<User> GetAllUsers()
	{
		if (!File.Exists(_filePath))
			return new List<User>();

		var json = File.ReadAllText(_filePath);

		return JsonSerializer.Deserialize<List<User>>(json) 
			?? new List<User>();
    }

	public void SaveUsers(List<User> users)
	{
		var json = JsonSerializer.Serialize(users, 
			new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText(_filePath, json);
    }

}
