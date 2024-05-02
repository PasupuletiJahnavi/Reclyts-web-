using Loginregister.Models;
using Microsoft.EntityFrameworkCore;



namespace Loginregister
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<UpdateInfo> UpdateInfos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
              
                entity.HasIndex(u => u.Email).IsUnique();
            });

            
        }
    }
}

