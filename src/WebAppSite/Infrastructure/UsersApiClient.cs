using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAppSite.Models;

namespace WebAppSite.Infrastructure
{
    public interface IUsersApiClient
    {
        Task<string> GetData(string path);
        Task PutData(string user, string path);
    }
    public class UsersApiClient : IUsersApiClient
    {
        private readonly HttpClient _client;
        public UsersApiClient (HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://users.api/");
            _client = httpClient;
        }
        public async Task<string> GetData(string path)
        {
            return await _client.GetStringAsync(requestUri: $"{path}");
        }
        public async Task PutData(string user, string path)
        {
            StringContent content = new StringContent(user, Encoding.UTF8, "application/json");
            await _client.PutAsync(requestUri: $"{path}", content);
        }
    }
}
