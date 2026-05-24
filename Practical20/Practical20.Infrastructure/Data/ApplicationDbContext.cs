namespace Practical20.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor _httpContextAccessor) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Role>().HasData(
            new Role()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                RoleName = "Admin",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = "system",
                IsActive = true
            },
            new Role()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                RoleName = "User",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = "system",
                IsActive = true
            });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userId = _httpContextAccessor.HttpContext?.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";

        foreach (var entry in ChangeTracker.Entries<IAuditEnitity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                    break;
            }
        }

        var auditEntries = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Unchanged)
                continue;

            var action = entry.State switch
            {
                EntityState.Added => "Created",
                EntityState.Deleted => "Deleted",
                _ => "Updated"
            };

            var oldValues = entry.State == EntityState.Modified
                ? JsonSerializer.Serialize(entry.OriginalValues.ToObject())
                : null;

            var newValues = entry.State != EntityState.Deleted
                ? JsonSerializer.Serialize(entry.CurrentValues.ToObject())
                : null;

            auditEntries.Add(new AuditLog
            {
                TableName = entry.Entity.GetType().Name,
                Action = action,
                EntityId = entry.Entity.Id.ToString(),
                OldValues = oldValues,
                NewValues = newValues,
                ChangedBy = userId,
                ChangedAt = DateTime.UtcNow
            });
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        if (auditEntries.Count != 0)
        {
            await AuditLogs.AddRangeAsync(auditEntries, cancellationToken);
            await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}
