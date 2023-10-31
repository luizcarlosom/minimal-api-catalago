using Microsoft.EntityFrameworkCore;
using MinimalAPICatalago.Models;

namespace MinimalAPICatalago.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        //Sobreescrevendo os valores convencionais e explicitando relacionamentos do entity framework
        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Category
            mb.Entity<Category>().HasKey(c => c.CategoryId);

            mb.Entity<Category>().Property(c => c.Name)
                                .HasMaxLength(100)
                                .IsRequired();

            mb.Entity<Category>().Property(c => c.Description)
                                .HasMaxLength(150)
                                .IsRequired();
        
            //Product
            mb.Entity<Product>().HasKey(c => c.ProductId);

            mb.Entity<Product>().Property(c => c.Name)
                                .HasMaxLength(100)
                                .IsRequired();

            mb.Entity<Product>().Property(c => c.Description)
                                .HasMaxLength(150);

            mb.Entity<Product>().Property(c => c.Image)
                                .HasMaxLength(100);

            mb.Entity<Product>().Property(c => c.Price)
                                .HasPrecision(14,2);

            //Relationship
            mb.Entity<Product>()
                .HasOne<Category>(c => c.Category)
                    .WithMany(c => c.Products)
                        .HasForeignKey(c => c.CategoryId);
        }
    }
}
