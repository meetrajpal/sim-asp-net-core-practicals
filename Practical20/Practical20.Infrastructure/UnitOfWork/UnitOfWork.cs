namespace Practical20.Infrastructure.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    public IUserRepository UserRepo { get; } = new UserRepository(context);
    public IRoleRepository RoleRepo { get; } = new RoleRepository(context);
    public IUserRoleRepository UserRoleRepo { get; } = new UserRoleRepository(context);
    public IStudentRepository StudentRepo { get; } = new StudentRepository(context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
