using WebApplication1.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        public DbSet<User> user {get; set;}

    }
}