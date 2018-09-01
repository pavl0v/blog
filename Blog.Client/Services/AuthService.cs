using Blog.Common.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string, string>("password", password)
            });

            var response = await Client.PostAsync("auth/token", formContent);
            if (!response.IsSuccessStatusCode)
                return new TokenDto { Name = login, Token = "" };

            var result = await response.Content.ReadAsJsonAsync<TokenDto>();

            return result;
        }
    }
}
