using Microsoft.EntityFrameworkCore;

namespace UserApi.DAL
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserContext()
        {

        }
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=KnowledgeGraph;uid=sa;pwd=wasd12345652");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
