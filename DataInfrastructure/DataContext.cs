using Microsoft.EntityFrameworkCore;
using StudentLibrary.Models;

namespace StudentLibrary.DataInfrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
