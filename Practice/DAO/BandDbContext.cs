using Practice.Models;
using Microsoft.EntityFrameworkCore;

namespace Practice.DAO
{
    public class BandDbContext : DbContext
    {
        public BandDbContext(DbContextOptions<BandDbContext> options)
            : base(options) {}
        public DbSet<BandModel> bands {get;set;}
        public DbSet<AlbumModel> albums {get;set;}
    }
}