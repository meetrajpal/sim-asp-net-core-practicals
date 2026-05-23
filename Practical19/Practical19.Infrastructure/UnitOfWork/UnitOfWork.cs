namespace Practical19.Infrastructure.UnitOfWork;

public class UnitOfWork(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : IUnitOfWork
{
    private readonly Lazy<IStudentRepository> _studentRepo = new(() => { return new StudentRepository(dbContext, httpContextAccessor); });
    public IStudentRepository StudentRepository => _studentRepo.Value;

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
    public void Dispose()
    {
        dbContext.Dispose();
    }
}
