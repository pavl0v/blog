using Blog.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Api.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<PostDto> Posts { get; set; }
    }
}
