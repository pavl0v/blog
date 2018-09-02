using System;
using System.Net.Http;

namespace Blog.Client.Services
{
    public abstract class ServiceBase
    {
        public HttpClient Client { get; }

        public ServiceBase(HttpClient client)
        {
            // TODO : put settings to config file
            client.BaseAddress = new Uri("http://localhost:5000/api/");
            //client.DefaultRequestHeaders.Add("Accept", "");
            //client.DefaultRequestHeaders.Add("User-Agent", "");

            Client = client;
        }
    }
}
