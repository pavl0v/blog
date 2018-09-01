using Blog.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.Interfaces
{
    public interface IUsersRepository
    {
        UserDto Get(string userId);
        UserDto Get(string login, string password);
    }
}
