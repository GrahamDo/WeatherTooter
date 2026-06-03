using System.Text;
using Newtonsoft.Json;

namespace WeatherTooter;

internal class MastodonApiClient(HttpClientFactory clientFactory)
{
    private readonly HttpClient _client = clientFactory.GetClient();

    private static string BuildBaseUrl(string instanceUrl)
    {
        var baseUrlSb = new StringBuilder();
        if (!instanceUrl.StartsWith("https://"))
            baseUrlSb.Append("https://");
        baseUrlSb.Append(instanceUrl);
        if (!instanceUrl.EndsWith("/"))
            baseUrlSb.Append("/");
        baseUrlSb.Append("api/v1/statuses");
        return baseUrlSb.ToString();
    }

    public async Task Post(string instanceUrl, string token, string text)
    {
        var status = new MastodonStatus
        {
            Status = text
        };
        
        var json = JsonConvert.SerializeObject(status);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var url = BuildBaseUrl(instanceUrl);
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = content,
            Headers = { Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token) }
        };

        using var response = await _client.SendAsync(request);        
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                throw new ApplicationException("Invalid Mastodon token");

            throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
        }
    }
}