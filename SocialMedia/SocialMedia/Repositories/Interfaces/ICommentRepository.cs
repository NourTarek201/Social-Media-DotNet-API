using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.ViewModel;

namespace SocialMedia.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> AlluserComments(Guid id);
        Task<List<Comment>> AllpostComments(Guid id);
    }
}
