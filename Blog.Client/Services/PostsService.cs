using Blog.Common.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blog.Common.Client
{
    public class PostsService : ServiceBase
    {
        public PostsService(HttpClient client) : base(client)
        {
            //
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
