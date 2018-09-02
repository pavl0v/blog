using Blog.Common.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Client.Services
{
    public class PostsService : ServiceBase
    {
        public PostsService(HttpClient client) : base(client)
        {
            //
        }

        public async Task<int> CreatePost(PostDto post, string token = null)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                // Set Authentication header with JWT value
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            // Allow service side to accept JSON
            //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("posts", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsJsonAsync<int>();
        }

        public async Task<IEnumerable<PostDto>> GetAllPosts(string token = null)
        {
            if(!string.IsNullOrWhiteSpace(token))
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.GetAsync("posts/all");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsJsonAsync<IEnumerable<PostDto>>();

            return result;
        }

        public async Task<IEnumerable<PostDto>> GetByPostId(string postId)
        {
            var response = await Client.GetAsync(string.Format("posts/{0}", postId));
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsJsonAsync<IEnumerable<PostDto>>();

            return result;
        }

        public async Task<IEnumerable<PostDto>> GetByTags(string tags, string token = null)
        {
            if (string.IsNullOrWhiteSpace(tags))
                return new List<PostDto>();

            if (!string.IsNullOrWhiteSpace(token))
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var arr = tags.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<PostDto> searchResult = new List<PostDto>();
            foreach(var tag in arr)
            { 
                var response = await Client.GetAsync(string.Format("posts/tag/{0}", tag));
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsJsonAsync<IEnumerable<PostDto>>();
                foreach (var post in result)
                {
                    if (searchResult.Any(x => x.Id == post.Id))
                        continue;
                    searchResult.Add(post);
                }
            }

            return searchResult;
        }

        public async Task<IEnumerable<PostDto>> GetByText(string text, string token = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<PostDto>();

            if (!string.IsNullOrWhiteSpace(token))
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            List<PostDto> searchResult = new List<PostDto>();
            var response = await Client.GetAsync(string.Format("posts/text/{0}", text));
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsJsonAsync<IEnumerable<PostDto>>();
            foreach (var post in result)
            {
                if (searchResult.Any(x => x.Id == post.Id))
                    continue;
                searchResult.Add(post);
            }

            return searchResult;
        }
    }
}
