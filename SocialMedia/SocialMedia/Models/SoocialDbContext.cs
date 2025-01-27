using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Models
{
    public class SoocialDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public SoocialDbContext(DbContextOptions<SoocialDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
        //public virtual DbSet<User>Users { get; set; }
        //public virtual DbSet<Comment> Comments { get; set; }
        //public virtual DbSet<Post> Posts { get; set; }
        //public virtual DbSet<Message> Messages { get; set; }
        //public virtual DbSet<Followers> Followers { get; set; }
      
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<chatroom> Chatrooms { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Likers)
                .WithMany(u => u.LikedPosts)
                .UsingEntity(j => j.ToTable("PostLikers"));

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.CreatedPosts)
                .HasForeignKey(p => p.UserId);
        }



    }

}
