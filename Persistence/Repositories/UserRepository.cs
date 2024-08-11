using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return _context.Users.SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username) => await _context.Users.SingleOrDefaultAsync(c => c.Username == username);

    public void Add(User user) => _context.Users.Add(user);

    public async Task<bool> IsUsernameUniqueAsync(string username) => !await _context.Users.AnyAsync(c => c.Username == username);
}
