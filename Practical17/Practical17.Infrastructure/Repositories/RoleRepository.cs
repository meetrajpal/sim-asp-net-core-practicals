using Practical17.Domain.Interfaces.Repositories;
using Practical17.Infrastructure.Data;

namespace Practical17.Infrastructure.Repositories;

public class RoleRepository(ApplicationDbContext dbContext) : BaseRepository<Role>(dbContext), IRoleRepository
{
    public async Task<Role?> FindByRoleNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.RoleName == name);
    }
}
