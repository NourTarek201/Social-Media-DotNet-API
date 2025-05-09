using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Models;
using SocialMedia.Repositories.Interfaces;
using SocialMedia.Services;
using SocialMedia.Servises;
using SocialMedia.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private ChatService _chatService;
        IBaseRepository<Chatroom> _chatRepository;
        IUserRepository<User> _userRepository;
        public ChatController(ChatService _chatService, IBaseRepository<Chatroom> _chatRepository,
            IUserRepository<User> userRepository)
        {
            this._chatService = _chatService;
            this._chatRepository = _chatRepository;
            this._userRepository = userRepository;
        }

        // // GET api/<ChatController>
        [HttpGet]
        public async Task<IActionResult> getAllFollowers()
        {
            var all = await _chatRepository.getAll();
            return Ok(all);
        }
       


        // POST api/<ChatController>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        //single chat controller
        public async Task<IActionResult> Post([FromBody] MessageRequestDTO request)
        {
            var userIdString = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new Exception("Please sign in first.");
            }
            if (!Guid.TryParse(userIdString, out Guid userGuid))
            {
                return BadRequest("Invalid user ID.");
            }

            User targetUser = await _userRepository.GetUserById(request.TargetUserId);
            if(targetUser == null)
            {
                return BadRequest("Invalid target user ID.");
            }

            List<User> users = new List<User>();
            users.Add(targetUser);

            var chatroom = await _chatService.CreateChatroom(userGuid, users, request.Msg);

            if (chatroom == null)
            {
                return BadRequest("Invalid request.");
            }
            return Ok("ChatRoom Created you can now send a message");
        }

    }
}
