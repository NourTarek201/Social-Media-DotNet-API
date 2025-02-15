using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.ViewModel;

namespace SocialMedia.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SocialDbContext _context;

        public CommentRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> AlluserComments(Guid id)
        {
            var usercomments = await _context.Comments.Where(x => x.UserId == id).ToListAsync();
            return usercomments;

        }
        public async Task<List<Comment>> AllpostComments(Guid id)
        {
            var postcomments = await _context.Comments.Where(x => x.PostId == id).ToListAsync();
            return postcomments;

        }


    }
}
