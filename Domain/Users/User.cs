using System.Text.Json.Serialization;

namespace Domain.User;

public class User
{
    public User(Guid id, string firstName, string lastName, string username, byte[]? passwordHash, byte[]? passwordSalt)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string Username { get; private set; } = string.Empty;

    [JsonIgnore]
    public byte[]? PasswordHash { get; set; }
    [JsonIgnore]
    public byte[]? PasswordSalt { get; set; }
}