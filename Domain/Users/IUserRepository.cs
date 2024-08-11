namespace Domain.User;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByUsernameAsync(string username);

    Task<bool> IsUsernameUniqueAsync(string username);

    void Add(User user);
}
