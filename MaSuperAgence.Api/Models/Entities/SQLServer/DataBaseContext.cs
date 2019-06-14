using Microsoft.EntityFrameworkCore;

namespace MaSuperAgence.Api.Models.Entities.SQLServer
{
  public partial class DataBaseContext : DbContext
  {
    public virtual DbSet<Photo> Photos { get; set; }
    public virtual DbSet<Property> Properties { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

      modelBuilder.Entity<Photo>(entity =>
      {
        entity.Property(e => e.Content).IsRequired();

        entity.Property(e => e.FileName)
                  .IsRequired()
                  .HasMaxLength(250);

        entity.HasOne(d => d.IdPropertyNavigation)
                  .WithMany(p => p.Photos)
                  .HasForeignKey(d => d.IdProperty)
                  .HasConstraintName("FK_Photos_Properties");
      });

      modelBuilder.Entity<Property>(entity =>
      {
        entity.Property(e => e.Category).HasMaxLength(250);

        entity.Property(e => e.Description).HasMaxLength(250);

        entity.Property(e => e.Surface).HasMaxLength(250);

        entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(250);
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.Property(e => e.Login)
                  .IsRequired()
                  .HasMaxLength(50);

        entity.Property(e => e.Password)
                  .IsRequired()
                  .HasMaxLength(50);
      });
    }
  }
}
