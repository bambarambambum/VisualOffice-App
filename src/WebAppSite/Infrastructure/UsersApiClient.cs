using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        // private readonly IOptions<Settings> _settings;
        public UsersApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            // Get env "UsersAPI"
            string apiName;
            if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("HOST_USERS_API")))
            {
                apiName = Environment.GetEnvironmentVariable("HOST_USERS_API");
            }
            else
            {
                apiName = configuration.GetSection("HOST_USERS_API").Value;
            }
            httpClient.BaseAddress = new Uri($"http://{apiName}");
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
