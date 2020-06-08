using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParserLib
{
    public class HtmlLoaderRosAtom
    {
        private readonly HttpClient client;
        private readonly string url;

        public HtmlLoaderRosAtom(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}";
        }

        public async Task<string> GetSourseByPage(int num)
        {
            var currentUrl = url.Replace("{num}", num.ToString());
            var response = await client.GetAsync(currentUrl);

            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }

        public async Task<string> GetSourseByPage(string num)
        {
            var currentUrl = url.Replace("{num}", num.ToString());
            var response   = await client.GetAsync(currentUrl);

            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
