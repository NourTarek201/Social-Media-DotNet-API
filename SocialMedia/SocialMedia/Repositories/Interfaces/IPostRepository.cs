using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.ViewModel;
using System.Threading.Tasks;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> AlluserPosts(Guid id);


    }
}
