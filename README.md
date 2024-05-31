# WeatherTooter

This is a console app designed to be run as a bot. It posts a specified location's current and forecast weather data to Mastodon. For an example implementation, see https://mastodon.africa/@EkurhuleniWeather

## Prerequisites

In order to use this bot, you'll need a Mastodon Access Token with permission to <code>write:statuses</code>. You can get one of those by going to Preferences -> Development in your Mastodon account.

## Getting it running

Pull the code and build it for your preferred platform. I've tested it on both Windows x64 and Ubuntu 24.04 x64.

Then open a terminal and run the following for each setting to configure everything:

<code>WeatherTooter --set &lt;setting&gt; &lt;value&gt;</code>

### Settings

* LocationLatitude - The latitude cordinate of the location to get weather data for (e.g. <code>-26.2278</code>)
* LocationLongitude - The longitude cordinate of the location to get weather data for (e.g. <code>28.1724</code>)
* LocationName - Descriptive name of the location (e.g. <code>Johannesburg</code>)
* IanaTimeZoneName - The IANA name of the time zone to get results in. For more information, see https://en.wikipedia.org/wiki/List_of_tz_database_time_zones (e.g. <code>Africa/Johannesburg</code>)
* HoursToForecast - Whole number between 0 and 255. The number of hours ahead to get a weather forecast for
* MastodonToken - Your Mastodon Access Token
* MastodonInstanceUrl - The URL of your Mastodon instance (e.g. <code>mastodon.africa</code>)
* TemplateFile - The name of the file to use as a template (default: <code>toot-template.txt</code>)

### Running

Once everything is set up, run:

<code>WeatherTooter --fake</code>

This will connect to the Weather API and print what it would toot to the console. If you're not happy with that, edit the <code>toot-template.txt</code>, which should be self-explanatory.

Once you're happy, run <code>WeatherTooter</code> without any arguments.

Then create a cron job or Windows Scheduled Task to do that as often as you like, and you're good to go! :-)

### Advanced: Custom Settings File

By default, WeatherTooter reads from and writes to a file called <code>settings.json</code> in the startup directory. You can change this by including the argument <code>--settingsFile &lt;FileName&gt;</code> when running the program. For example:

<code>WeatherTooter --set LocationName Johannesburg --settingsFile johannesburg-settings</code>

This allows you to have multiple profiles, so that you can run multiple weather bots at the same time.

In the above example, once you've configured all the settings for Johannesburg, you can run:

<code>WeatherTooter --fake --settingsFile johannesburg-settings</code>

to verify that it meets your expectations. When you're ready to run it "for real", just execute:

<code>WeatherTooter --settingsFile johannesburg-settings</code>
