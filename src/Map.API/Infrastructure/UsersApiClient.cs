using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace My.Api.Infrastructure
{
    public interface IUsersApiClient
    {
        Task<string> GetData(string path);
    }
    public class UsersApiClient : IUsersApiClient
    {
        private readonly HttpClient _client;
        public UsersApiClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://users.api/");
            _client = httpClient;
        }
        public async Task<string> GetData(string path)
        {
            return await _client.GetStringAsync(requestUri: $"{path}");
        }
    }
}
