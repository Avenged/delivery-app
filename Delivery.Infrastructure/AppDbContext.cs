using Delivery.Domain;
using Delivery.Infrastructure.Converters;
using Delivery.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<CustomerAggregate> Customers { get; set; } = null!;

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=myuser;Password=mypassword;Database=mydatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerAggregate>(ConfigureCustomerTable);

        AttachGlobalConverters(modelBuilder);
    }

    private static void AttachGlobalConverters(ModelBuilder modelBuilder)
    {
        TrimmedNonEmptyStringConverter converter = new();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(TneString))
                {
                    property.SetValueConverter(converter);
                    property.SetColumnType("text");
                }
            }
        }
    }

    private static void ConfigureCustomerTable(EntityTypeBuilder<CustomerAggregate> builder)
    {
        builder.ToTable("customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.OwnsOne(c => c.Name, pn =>
        {
            pn.Property(p => p.GivenName).HasColumnName("given_name").IsRequired();
            pn.Property(p => p.FamilyName).HasColumnName("family_name").IsRequired();
            pn.Property(p => p.MiddleName).HasColumnName("middle_name");
            pn.Property(p => p.Prefix).HasColumnName("prefix");
            pn.Property(p => p.Suffix).HasColumnName("suffix");
        });

        builder.OwnsOne(c => c.PhoneNumber, pn =>
        {
            pn.Property(p => p.Value).HasColumnName("phone_number").IsRequired();
        });

        builder.OwnsOne(c => c.Address, addr =>
        {
            addr.Property(a => a.Street).HasColumnName("street").IsRequired();
            addr.Property(a => a.City).HasColumnName("city").IsRequired();
            addr.Property(a => a.State).HasColumnName("state").IsRequired();
            addr.Property(a => a.PostalCode).HasColumnName("postal_code").IsRequired();
            addr.Property(a => a.Country).HasColumnName("country").IsRequired();
        });

        builder.Property(c => c.Emails)
            .HasColumnName("emails")
            .HasConversion(new EmailListToJsonConverter())
            .HasColumnType("json")
            .IsRequired(false);

        // DateOfBirth
        builder.Property(c => c.DateOfBirth)
            .HasColumnName("date_of_birth")
            .HasColumnType("date")
            .IsRequired();

        // CreatedAt y UpdatedAt
        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        // builder.Property(c => c.CreatedAt).ValueGeneratedOnAdd();
        // builder.Property(c => c.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    }
}
