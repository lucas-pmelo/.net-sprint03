using Microsoft.EntityFrameworkCore;
using Sprint03.domain.model;

namespace Sprint03.adapter.output.database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        
    }
}