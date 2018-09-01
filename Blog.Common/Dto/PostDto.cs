using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Dto
{
    public class PostDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
