namespace Practical20.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
{
    public new async Task<ApiResponse<List<User>>> GetAllAsync()
    {
        var data = await _dbSet.Include(x => x.UserRoles).ThenInclude(x => x.Role).ToListAsync();

        return ApiResponse<List<User>>.Success(data);
    }

    public new async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbSet.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _dbSet.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(e => e.Email == email);
    }
}
