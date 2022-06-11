using Microsoft.EntityFrameworkCore;

namespace Api.DAL
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
            optionsBuilder.UseSqlServer("server=localhost;database=KnowledgeGraph;uid=sa;pwd=password");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(m =>
            {
                m.ToTable("Users");
                m.Property(p => p.UserName).HasMaxLength(20);
                m.Property(p => p.Password).HasMaxLength(20);
                m.Property(p => p.Name).HasMaxLength(10);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
