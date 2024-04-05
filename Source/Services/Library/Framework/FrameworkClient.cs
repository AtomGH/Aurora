using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aurora.Library.Framework
{
    public class FrameworkClient : IDisposable
    {
        private readonly HttpClient _client;

        public FrameworkClient()
        {
            _client = new();
        }

        public async Task<InstanceInformation> RegisterAsync()
        {
            HttpResponseMessage response = await _client.PostAsync("http://localhost:5000/instances", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InstanceInformation>();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
