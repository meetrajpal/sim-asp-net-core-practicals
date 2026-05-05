namespace Practical17.Infrastructure.Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.Email).HasColumnType("varchar(256)").IsRequired();
        builder.HasIndex(x => x.Email).IsUnique().HasDatabaseName("IX_Unique_User_Email");

        builder.Property(x => x.PasswordHash).HasMaxLength(300).IsRequired();

        builder.Property(u => u.Name).HasMaxLength(200).IsRequired();

        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.Property(u => u.RefreshToken).HasMaxLength(500);

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}
