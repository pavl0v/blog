using Blog.Common.Dto;
using Blog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Data.Mocks
{
    public class UsersRepositoryMock : IUsersRepository
    {
        private readonly List<UserDto> _users;

        public UsersRepositoryMock()
        {
            _users = new List<UserDto>();
            _users.Add(new UserDto { UserId = "1", Login = "user1", Password = "password1" });
            _users.Add(new UserDto { UserId = "2", Login = "user2", Password = "password2" });
            _users.Add(new UserDto { UserId = "3", Login = "user3", Password = "password3" });
        }

        public UserDto Get(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            return _users.FirstOrDefault(x => x.UserId == userId);
        }

        public UserDto Get(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            return _users.FirstOrDefault(x => 
                x.Login.ToLower() == login.ToLower() &&
                x.Password == password);
        }
    }
}
