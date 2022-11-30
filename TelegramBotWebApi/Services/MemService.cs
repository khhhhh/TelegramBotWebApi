using System.Collections.Generic;

namespace TelegramBotWebApi.Services
{
    public class PicsResponse
    {
        public string url;
        public string template;
    }
    public class MemService
    {
        IHttpClientFactory _factory;
        public MemService(IHttpClientFactory _factory)
        {
            this._factory = _factory;
        }

        public async Task<IEnumerable<string>?> GetAllPics()
        {
            var client = _factory.CreateClient("memescreator");
            var request = new HttpRequestMessage(HttpMethod.Get, "images");
            client.Timeout = TimeSpan.FromSeconds(5);
            HttpResponseMessage response = await client.SendAsync(request);

            var pics = await response.Content.ReadFromJsonAsync<IEnumerable<PicsResponse>>();
            return pics?.Select(x => x.url);
        }
    }
}
