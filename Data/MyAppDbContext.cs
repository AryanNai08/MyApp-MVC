using Microsoft.EntityFrameworkCore;
using MyApp_MVC.Models;

namespace MyApp_MVC.Data
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}
