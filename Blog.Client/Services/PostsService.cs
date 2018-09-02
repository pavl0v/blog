using Blog.Common.Dto;
using Newtonsoft.Json;
using System.Collections.Generic;
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
    }
}
