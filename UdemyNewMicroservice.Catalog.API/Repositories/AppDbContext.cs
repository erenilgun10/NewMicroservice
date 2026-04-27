using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;
using UdemyNewMicroservice.Catalog.API.Features.Categories;
using UdemyNewMicroservice.Catalog.API.Features.Courses;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;


        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
            
            return new AppDbContext(optionsBuilder.Options);

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
