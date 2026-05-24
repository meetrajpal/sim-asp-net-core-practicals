namespace Practical20.Infrastructure.Repositories;

public class UserRoleRepository(ApplicationDbContext dbContext) : BaseRepository<UserRole>(dbContext), IUserRoleRepository
{
}
