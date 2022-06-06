using Library.Core.Entities;
using Library.Core.Interfaces.IRepository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly HttpClient _client;
        public SecurityRepository(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Security> GetLoginByCredentials(string path, UserLogin login)
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var Result = await response.Content.ReadAsStringAsync();
                return (JsonConvert.DeserializeObject<List<Security>>(Result)).Find(f => f.Password == login.Password && f.UserName == login.UserName);
            }
            return new Security();

        }
    }
}
