using System.Data.Entity;

namespace WebApplication1test1.Models
{
    public class Class1Context : DbContext
    {
        public DbSet<Class1> Class1S { get; set; }
    }
}