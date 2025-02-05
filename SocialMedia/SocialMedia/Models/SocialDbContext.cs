using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Models
{
    public class SocialDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public SocialDbContext(DbContextOptions<SocialDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Post>()
            //    .HasMany(p => p.Likers)
            //    .WithMany(u => u.LikedPosts)
            //    .UsingEntity(j => j.ToTable("PostLikers"));

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.CreatedPosts)
                .HasForeignKey(p => p.UserId);


            //User UserFollower
            modelBuilder.Entity<UserFollower>()
            .HasOne(uf => uf.User)
            .WithMany(u => u.Followers)
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollower>()
            .HasOne(uf => uf.Follower)
            .WithMany(u => u.Followings)
            .HasForeignKey(uf => uf.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);


            //UserReaction
            modelBuilder.Entity<UserReaction>()
            .HasOne(uf => uf.Post)
            .WithMany(u => u.Reacters)
            .HasForeignKey(uf => uf.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Chatroom> Chatrooms { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<UserReaction> UserReaction { get; set; }
        public virtual DbSet<UserFollower> Followers { get; set; }

        


    }

}
