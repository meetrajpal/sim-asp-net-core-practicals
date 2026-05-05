namespace Practical17.Domain.Interfaces.Repositories;

public interface IUserRoleRepository : IBaseRepository<UserRole>
{
    Task AddUserToRoleAsync(Guid userId, Guid roleId);
}
