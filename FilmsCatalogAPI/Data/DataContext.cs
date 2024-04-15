using FilmsCatalogAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FilmsCatalogAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        public DbSet<Film> Films { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FilmCategory> FilmCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmCategory>()
                .HasKey(fc => fc.Id);
            modelBuilder.Entity<FilmCategory>()
                .HasOne(f => f.Film)
                .WithMany(fc => fc.FilmCategory)
                .HasForeignKey(f => f.FilmId);
            modelBuilder.Entity<FilmCategory>()
                .HasOne(c => c.Category)
                .WithMany(fc => fc.FilmCategory)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}
