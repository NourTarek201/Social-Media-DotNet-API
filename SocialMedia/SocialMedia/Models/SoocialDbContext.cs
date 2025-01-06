using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Models
{
    public class SoocialDbContext : IdentityDbContext<BaseEntity>
    {
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
        //public virtual DbSet<User>Users { get; set; }
        //public virtual DbSet<Comment> Comments { get; set; }
        //public virtual DbSet<Post> Posts { get; set; }
        //public virtual DbSet<Message> Messages { get; set; }
        //public virtual DbSet<Followers> Followers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=sochialtest;User Id=Admin;Password=Admin_123;Integrated Security=True;Encrypt=False");
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<chatroom> Chatrooms { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }



    }

}
