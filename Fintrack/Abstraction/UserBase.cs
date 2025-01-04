using System.IO;
using System.Collections.Generic;
using Fintrack.Model;

namespace Fintrack.Abstraction
{
    public abstract class UserBase
    {
        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "users.csv");

        protected List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();

            var users = new List<User>();
            var lines = File.ReadAllLines(FilePath);

            // Skip the header line
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    users.Add(new User
                    {
                        UserId = Guid.Parse(parts[0]),
                        UserName = parts[1],
                        Password = parts[2]
                    });
                }
            }

            return users;
        }

        protected void SaveUsers(List<User> users)
        {
            // Write header and user data to the file
            var lines = new List<string> { "UserId,UserName,Password" };
            lines.AddRange(users.Select(user => $"{user.UserId},{user.UserName},{user.Password}"));
            File.WriteAllLines(FilePath, lines);
        }
    }
}
