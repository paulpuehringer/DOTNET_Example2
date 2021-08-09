using Microsoft.EntityFrameworkCore;
using DOTNET_Example.Models;

namespace DOTNET_Example.Models
{
    //class for entity framework postgres plugin
    public class MyWebApiContext : DbContext
    {
        public MyWebApiContext(DbContextOptions<MyWebApiContext> options) : base (options) { }
        
        public DbSet<Pizza> Pizzas { get; set; }
    }
}