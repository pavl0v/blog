using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Api.ViewModels;
using Blog.Client.Services;
using Blog.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class SearchController : Controller
    {
        private readonly PostsService _postsService;

        public SearchController(PostsService postsService)
        {
            _postsService = postsService;
        }

        public IActionResult Index()
        {
            return View(new SearchViewModel { Posts = new List<PostDto>() });
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel model)
        {
            var posts = new List<PostDto>();
            if (model == null)
                return View(model);

            IEnumerable<PostDto> searchResult;
            if (!string.IsNullOrWhiteSpace(model.Tags))
            {
                searchResult = await _postsService.GetByTags(model.Tags, Request.Cookies["token"]);
                DistinctPosts(posts, searchResult);
            }
            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                searchResult = await _postsService.GetByText(model.Text, Request.Cookies["token"]);
                DistinctPosts(posts, searchResult);
            }
            if (!string.IsNullOrWhiteSpace(model.Username))
            {
                searchResult = await _postsService.GetByUsername(model.Username, Request.Cookies["token"]);
                DistinctPosts(posts, searchResult);
            }

            model.Posts = posts.ToList();

            return View(model);
        }

        private void DistinctPosts(List<PostDto> posts, IEnumerable<PostDto> searchResult)
        {
            foreach(var sr in searchResult)
            {
                if (posts.Any(x => x.Id == sr.Id))
                    continue;
                posts.Add(sr);
            }
        }
    }
}