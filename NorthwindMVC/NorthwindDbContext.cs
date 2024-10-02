using Microsoft.EntityFrameworkCore;

namespace NorthwindMVC
{
    //a new class for the db inhereted from the dbcontext class
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
