using Microsoft.EntityFrameworkCore;

namespace Users.API.Models.Context
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {

        }

        // Таблицы в MySQL
        public DbSet<User> Users { get; set; }
        public DbSet<RP> RPs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("VisualOfficeDev");
        //}
    }
}
