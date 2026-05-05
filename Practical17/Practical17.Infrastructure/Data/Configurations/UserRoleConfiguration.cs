namespace Practical17.Infrastructure.Data.Configurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);

        builder.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique().HasDatabaseName("IX_Unique_UserRole_UserId_RoleId");

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}
