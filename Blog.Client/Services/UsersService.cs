using Blog.Common.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Client.Services
{
    public class UsersService : ServiceBase
    {
        public UsersService(HttpClient client) : base (client)
        {
            //
        }

        public async Task<UserDto> Get(string userId)
        {
            var response = await Client.GetAsync(string.Format("users/{0}", userId));
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsJsonAsync<UserDto>();

            return result;
        }
    }
}
