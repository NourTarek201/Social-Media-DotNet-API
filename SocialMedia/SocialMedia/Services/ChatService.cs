using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Models;
using SocialMedia.Models.Enums;
using SocialMedia.Repositories;
using SocialMedia.Repositories.Interfaces;
using System.Linq;

namespace SocialMedia.Servises
{
    public class ChatService
    {
        IBaseRepository<Chatroom> _chatroomRepository;
        IUserRepository<User> _userRepository;
        FollowerRepository _followerRepository;

        public ChatService(IUserRepository<User> userRepository, FollowerRepository _followerRepository,
            IBaseRepository<Chatroom> _chatroomRepository)
        {
            _userRepository = userRepository;
            this._followerRepository =_followerRepository;
            this._chatroomRepository = _chatroomRepository;
        }

        //single chatroom only !
        public async Task<Chatroom> CreateChatroom(Guid creatorId, List<User> users, String msg)
        {
            User creator = await _userRepository.GetUserById(creatorId);
            if (creator == null)
                return null;

            if (users.Contains(creator))
            {
                throw new Exception("User cant send to him/herself");
            }

            //checking if creator in followers of all users
            foreach (var u in users)
            {
                if (u == null)
                {
                    throw new Exception("User cannot be null");
                }
                else
                {
                    UserFollower userFollower = await _followerRepository.GetUserFollowerByUsers(u.Id, creatorId);

                    if (userFollower == null)
                    {
                        throw new Exception("User is not a follower, Can't create Chatroom");
                    }
                    else
                    {
                        //check request status
                        if (userFollower.Status != RequestStatus.Accepted)
                        {
                            {
                                throw new Exception("User is not a follower, Can't create Chatroom");
                            }
                        }
                    }

                }
            }
            users.Add(creator);
            Chatroom chatroom = new Chatroom
            {
                Users = users,
                Messages = new List<Message>()
            };
            Message message = new Message();

            if (!msg.IsNullOrEmpty())
            {
                message.Content = msg;
                message.SenderId = creatorId;
                message.Sender = creator;
                chatroom.Messages.Add(message);
            }
            
            //creator.CreatedChatrooms.Add(chatroom);
            //chatroom.Creator = creator;

            return await _chatroomRepository.add(chatroom);
        }



    }
}
