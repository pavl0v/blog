using Blog.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Api.ViewModels
{
    public class SearchViewModel
    {
        public string Text { get; set; }
        public string Tags { get; set; }
        public string Username { get; set; }
        public List<PostDto> Posts { get; set; }
    }
}
