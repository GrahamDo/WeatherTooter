using RestSharp.Authenticators.OAuth2;
using RestSharp;
using System.Text;

namespace WeatherTooter;

internal class MastodonApiClient
{
    private RestClient _restClient = null!;

    private void InitialiseClient(string instanceUrl, string token)
    {
        if (string.IsNullOrEmpty(instanceUrl))
            throw new ApplicationException("Missing Mastodon instance URL");
        if (string.IsNullOrEmpty(token))
            throw new ApplicationException("Missing Mastodon token");

        var baseUrl = BuildBaseUrl(instanceUrl);
        var options = new RestClientOptions(baseUrl)
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer")
        };
        _restClient = new RestClient(options);
    }

    private static string BuildBaseUrl(string instanceUrl)
    {
        var baseUrlSb = new StringBuilder();
        if (!instanceUrl.StartsWith("https://"))
            baseUrlSb.Append("https://");
        baseUrlSb.Append(instanceUrl);
        if (!instanceUrl.EndsWith("/"))
            baseUrlSb.Append("/");
        baseUrlSb.Append("api/v1/");
        return baseUrlSb.ToString();
    }


    public async Task Post(string instanceUrl, string token, string text)
    {
        InitialiseClient(instanceUrl, token);
        var status = new MastodonStatus
        {
            Status = text
        };
        var request = new RestRequest("statuses", Method.Post).AddJsonBody(status);
        try
        {
            await _restClient.PostAsync(request);
        }
        catch (HttpRequestException ex)
        {
            if (ex.Message.Contains("Forbidden"))
                throw new ApplicationException("Invalid Mastodon token");

            throw;
        }
    }
}