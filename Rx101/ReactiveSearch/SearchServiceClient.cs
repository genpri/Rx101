using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ReactiveSearch
{
    internal class SearchServiceClient
    {
        private const string BaseAddress = "http://localhost.fiddler:2458/api/Search?searchTerm=";
        private readonly HttpClient _httpClient;

        public SearchServiceClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));

            var t = SearchAsync(searchTerm: "rx"); //making a request to warm the server
        }

        public async Task<IEnumerable<string>> SearchAsync(string searchTerm)
        {
            var response = await _httpClient.GetAsync(BaseAddress + searchTerm);
            return await response.Content.ReadAsAsync<IEnumerable<string>>();
        }
    }
}