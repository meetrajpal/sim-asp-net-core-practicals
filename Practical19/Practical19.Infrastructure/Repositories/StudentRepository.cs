namespace Practical19.Infrastructure.Repositories;

public class StudentRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : BaseRepository<Student>(dbContext, httpContextAccessor), IStudentRepository
{
    public override Task CreateAsync(Student entity)
    {
        entity.IsActive = true;
        return base.CreateAsync(entity);
    }
    public override Task DeleteAsync(Student entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }
}
