namespace Practical18.Infrastructure.UnitOfWork;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private readonly Lazy<IStudentRepository> _studentRepo = new(() => { return new StudentRepository(dbContext); });
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
