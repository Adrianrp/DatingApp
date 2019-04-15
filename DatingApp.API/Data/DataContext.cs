using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        // Properties, we give it the type of Value, which is located in Models>Value.cs
        // By convention we pluralize the name of the entities, and this is what is going to be called in the Db Table
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users  {get; set; }
         
    }
}