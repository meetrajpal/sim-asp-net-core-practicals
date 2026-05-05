using Practical17.Domain.Interfaces.Repositories;
using Practical17.Infrastructure.Data;

namespace Practical17.Infrastructure.Repositories;

public class UserRoleRepository(ApplicationDbContext dbContext) : BaseRepository<UserRole>(dbContext), IUserRoleRepository
{
    public async Task AddUserToRoleAsync(Guid userId, Guid roleId)
    {
        await _dbSet.AddAsync(new UserRole() { RoleId = roleId, UserId = userId });
    }
}
