namespace Practical20.Domain.Interfaces.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role?> FindByRoleNameAsync(string name);
}
