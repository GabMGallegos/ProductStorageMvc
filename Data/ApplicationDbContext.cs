using Microsoft.EntityFrameworkCore;
using ProductStorageMvc.Models;

namespace ProductStorageMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasKey(product => product.Id);

                entity.Property(product => product.Id)
                    .HasColumnName("id");

                entity.Property(product => product.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(product => product.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(10,2)")
                    .IsRequired();

                entity.Property(product => product.Quantity)
                    .HasColumnName("quantity")
                    .IsRequired();
            });
        }
    }
}
