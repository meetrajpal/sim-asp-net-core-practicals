namespace Practical20.Infrastructure.Data.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.RoleName).HasColumnType("varchar(100)").IsRequired();
        builder.HasIndex(x => x.RoleName).IsUnique().HasDatabaseName("IX_Unique_Role_RoleName");

        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.Property(x => x.CreatedBy)
        .HasMaxLength(450)
        .IsRequired(true);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(450)
            .IsRequired(false);

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}
