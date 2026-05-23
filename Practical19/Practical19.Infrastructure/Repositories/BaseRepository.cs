namespace Practical19.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
{

    protected readonly ApplicationDbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task CreateAsync(TEntity entity)
    {
        var userId = httpContextAccessor.HttpContext?
            .User?
            .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
            .Value;
        Console.WriteLine("maro leeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee =>>>>>>>>>>>>>>>>>>>>>>>>>  " + userId);
        if (!string.IsNullOrEmpty(userId))
            entity.CreatedById = Guid.Parse(userId);

        await _dbSet.AddAsync(entity);
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        var userId = httpContextAccessor.HttpContext?
            .User?
            .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
            .Value;
        if (!string.IsNullOrEmpty(userId))
            entity.UpdatedById = Guid.Parse(userId);
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        var userId = httpContextAccessor.HttpContext?
            .User?
            .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
            .Value;
        if (!string.IsNullOrEmpty(userId))
            entity.UpdatedById = Guid.Parse(userId);
        entity.IsActive = false;
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }
}
