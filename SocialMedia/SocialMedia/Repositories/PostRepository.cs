using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.ViewModel;

namespace SocialMedia.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _context;

        public PostRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> AlluserPosts(Guid id)
        {
            var usercomments = await _context.Posts.Where(x => x.UserId == id).ToListAsync();
            return usercomments;

        }
       

    }
}
