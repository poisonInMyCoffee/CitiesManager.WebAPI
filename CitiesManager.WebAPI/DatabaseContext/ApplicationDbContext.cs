using CitiesManager.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.WebAPI.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public virtual DbSet<City> Cities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasData(new City() { CityID = Guid.Parse("B4F121AB-0C63-4567-935A-9ABA64EE99AA"), CityName= "Banaras" });
            modelBuilder.Entity<City>().HasData(new City() { CityID = Guid.Parse("6738C94D-BDEA-4889-BA64-9B327BCA7766"), CityName="Lucknow" });
        }
    }
}
