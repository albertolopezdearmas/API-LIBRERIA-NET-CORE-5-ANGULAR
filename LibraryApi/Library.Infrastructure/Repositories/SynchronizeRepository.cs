using Library.Core.Entities;
using Library.Core.Interfaces.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class SynchronizeRepository : ISynchronizeRepository
    {
        private readonly HttpClient _client;
        public SynchronizeRepository(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Book>> GetBookApi(string path)
        {
            
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var Result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Book>>(Result);
            }
            return new List<Book>();
        }
        public async Task<List<Author>> GetAuthorApi(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var Result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Author>>(Result);
            }
            return new List<Author>();
        }

        public void Dispose()
        {
            if (_client != null) _client.Dispose();
        }
    }
}
