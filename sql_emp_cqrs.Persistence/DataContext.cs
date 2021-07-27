using Microsoft.EntityFrameworkCore;
using sql_emp_cqrs.Domain;

namespace sql_emp_cqrs.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employee {get; set;}
    }
}