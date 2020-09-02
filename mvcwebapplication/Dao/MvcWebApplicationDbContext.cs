using mvcwebapplication.Models;
using Microsoft.EntityFrameworkCore;

namespace mvcwebapplication.Dao
{
    public class MvcWebApplicationDbContext : DbContext
    {
        public MvcWebApplicationDbContext(DbContextOptions<MvcWebApplicationDbContext> options)
            : base(options) {}

        // Add DBSet(s) here
    }
}