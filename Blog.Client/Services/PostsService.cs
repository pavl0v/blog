using Blog.Common.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Blog.Client.Services
{
    public class PostsService : ServiceBase
    {
        public PostsService(HttpClient client) : base(client)
        {
            //
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
    }
}
