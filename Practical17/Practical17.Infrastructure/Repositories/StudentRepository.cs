using Practical17.Domain.Interfaces.Repositories;
using Practical17.Infrastructure.Data;

namespace Practical17.Infrastructure.Repositories;

public class StudentRepository(ApplicationDbContext dbContext) : BaseRepository<Student>(dbContext), IStudentRepository
{
    public async Task<Student?> FindByGRAsync(long gr)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.GRNumber == gr);
    }
}
