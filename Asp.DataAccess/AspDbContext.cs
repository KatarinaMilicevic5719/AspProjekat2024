using System;
using Asp.Domain;
using Microsoft.EntityFrameworkCore;

namespace Asp.DataAccess
{
    public class AspDbContext : DbContext
    {
        public AspDbContext(DbContextOptions options) : base(options)
        {

        }

        public string User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId, x.UseCaseId });

            base.OnModelCreating(modelBuilder);
        }
        
      //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      // {
      //     optionsBuilder.UseSqlServer("Data Source=DESKTOP-INTENQ7\\SQLEXPRESS;Initial Catalog=aspProj;Integrated Security=True;Trust Server Certificate=True")
      //          .UseLazyLoadingProxies();
      //  }

        public override int SaveChanges()
        {
            foreach(var entry in this.ChangeTracker.Entries())
            {
                if(entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = User;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            e.IsActive = false;
                            e.DeletedAt = DateTime.UtcNow;
                            e.DeletedBy = User;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFriends> UserFriends { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
        public object UseSqlServer { get; set; }
    }
}
