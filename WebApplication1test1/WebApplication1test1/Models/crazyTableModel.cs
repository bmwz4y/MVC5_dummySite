namespace WebApplication1test1.Models
{
    using System.Data.Entity;

    public class CrazyTableModel : DbContext
    {
        public CrazyTableModel()
            : base("name=crazyTableModel")
        {
        }

        public virtual DbSet<CrazyTable> CrazyTables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrazyTable>()
                .Property(e => e.Crazy)
                .IsUnicode(false);
        }
    }
}
