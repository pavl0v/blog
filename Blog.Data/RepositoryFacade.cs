using Blog.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public class RepositoryFacade
    {
        public IPostsRepository Posts { get; set; }
        public IUsersRepository Users { get; set; }

        public RepositoryFacade(
            IPostsRepository posts,
            IUsersRepository users)
        {
            Posts = posts;
            Users = users;
        }
    }
}
