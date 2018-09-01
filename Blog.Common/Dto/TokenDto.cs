using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common.Dto
{
    public class TokenDto
    {
        [JsonProperty("username")]
        public string Name { get; set; }

        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}
