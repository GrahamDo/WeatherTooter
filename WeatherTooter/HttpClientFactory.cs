namespace WeatherTooter;

internal class HttpClientFactory
{
    private HttpClient _client = null!; // Initialized in GetClient when first called
    private bool _isClientInitialised;
    
    public HttpClient GetClient()
    {
        if (!_isClientInitialised)
        {
            _client = new HttpClient();
            _isClientInitialised = true;
        }

        return _client;
    }        
}