namespace Persistence
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class PersistenceContext : DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(u => u.OwnerPosts).WithOne(p => p.OwnerUser);
            modelBuilder.Entity<User>().HasMany(u => u.WriterPosts).WithOne(p => p.WriterUser);

            User firstUser = new User()
            {
                Name = "Paco",
                Mail = "paco@gmail.com",
                Password = "1234",
            };

            User secondUser = new User()
            {
                Name = "Vicente",
                Mail = "vicente@gmail.com",
                Password = "1234"
            };

            modelBuilder.Entity<User>().HasData(
                firstUser,
                secondUser
            );

            modelBuilder.Entity<Post>().HasData(
                new Post()
                {
                    Text = "Hola amigo!",
                    OwnerUserId = firstUser.Id,
                    WriterUserId = secondUser.Id,
                    DateTime = new DateTime(2019, 12, 12),
                });
        }
    }
}