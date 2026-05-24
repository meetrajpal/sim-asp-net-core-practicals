namespace Practical20.Infrastructure.Data.Configurations;

public class StudentConfigurations : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.FirstName).HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(x => x.LastName).HasColumnType("nvarchar(100)").IsRequired();

        builder.Property(x => x.GRNumber).HasColumnType("decimal(10, 0)").IsRequired();
        builder.HasIndex(x => x.GRNumber).IsUnique().HasDatabaseName("IX_Unique_Student_GRNumber");

        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.CreatedBy)
        .HasMaxLength(450)
        .IsRequired(true);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(450)
            .IsRequired(false);
    }
}
