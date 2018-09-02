using Blog.Common.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Client.Services
{
    public class AuthService : ServiceBase
    {
        public AuthService(HttpClient client) : base(client)
        {
            //
        }

        public async Task<TokenDto> GetToken(string login, string password)
        {
            // Allow service side to accept JSON
            //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var user = new UserDto { Login = login, Password = password };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("auth/token", content);
            if (!response.IsSuccessStatusCode)
                return new TokenDto { Name = login, Token = "" };

            return await response.Content.ReadAsJsonAsync<TokenDto>();
        }
    }
}
