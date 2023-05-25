# WeatherTooter

This is a console app designed to be run as a bot. It posts a specified location's current and forecast weather data to Mastodon. For an example implementation, see https://mastodon.africa/@EkurhuleniWeather

## Prerequisite

In order to use this bot, you'll need a Mastodon Access Token with permission to <code>write:statuses</code>. You can get one of those by going to Preferences -> Development in your Mastodon account.

## Getting it running

Pull the code and build it for your preferred platform. I've tested it on both Windows x64 and Ubuntu 23.04 x64.

Edit the <code>toot-template.txt</code> file and make sure you're happy with what the toot will look like.

Then open a terminal and run the following for each setting to configure everything:

<code>WeatherTooter --set &lt;setting&gt; &lt;value&gt;</code>

### Settings

* LocationLatitude - The latitude cordinate of the location to get weather data for (e.g. <code>-26.2278</code>)
* LocationLongitude - The longitude cordinate of the location to get weather data for (e.g. <code>28.1724</code>)
* LocationName - Descriptive name of the location (e.g. <code>Johannesburg</code>)
* HoursToForecast - Whole number between 0 and 255. The number of hours ahead to get a weather forecast for
* MastodonToken - Your Mastodon Access Token
* MastodonInstanceUrl - The URL of your Mastodon instance (e.g. <code>mastodon.africa</code>)

### Running

You're done. Execute <code>WeatherTooter</code> without any arguments.

Then create a cron job or Windows Scheduled Task to do that as often as you like, and you're good to go! :-)
