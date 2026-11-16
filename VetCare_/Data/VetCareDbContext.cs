
using Microsoft.EntityFrameworkCore;
using VetCare.Models;

namespace VetCare.Data
{
    public class VetCareDbContext : DbContext
    {
        public VetCareDbContext(DbContextOptions<VetCareDbContext> options)
            : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Muayene> Muayeneler { get; set; }
    }
}
    