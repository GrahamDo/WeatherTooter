using Newtonsoft.Json;

namespace WeatherTooter
{
    internal class Settings
    {
        private const string SettingsFileName = "settings.json";
        
        public float LocationLatitude { get; set; }
        public float LocationLongitude { get; set; }
        public string LocationName { get; set; }
        [JsonIgnore]
        public string LocationNameNoSpaces => LocationName.Replace(" ", "");
        public byte HoursToForecast { get; set; }
        public string MastodonToken { get; set; }
        public string MastodonInstanceUrl { get; set; }

        public Settings()
        {
            LocationName = string.Empty;
            MastodonToken = string.Empty;
            MastodonInstanceUrl = string.Empty;
        }

        public static Settings Load()
        {
            if (!File.Exists(SettingsFileName))
                return new Settings();

            var text = File.ReadAllText(SettingsFileName);
            return JsonConvert.DeserializeObject<Settings>(text) ??
                   throw new ApplicationException($"Your '{SettingsFileName}' appears to be empty or corrupt.");
        }

        public void Save()
        {
            var serialised = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(SettingsFileName, serialised);
        }

        public void SetValueFromArguments(string setting, string value)
        {
            try
            {
                switch (setting.ToLower())
                {
                    case "locationlatitude":
                        LocationLatitude = float.Parse(value);
                        break;
                    case "locationlongitude":
                        LocationLongitude = float.Parse(value);
                        ;
                        break;
                    case "locationname":
                        LocationName = value;
                        break;
                    case "hourstoforecast":
                        HoursToForecast = byte.Parse(value);
                        break;
                    case "mastodontoken":
                        MastodonToken = value;
                        break;
                    case "mastodoninstanceurl":
                        MastodonInstanceUrl = value;
                        break;
                    default:
                        throw new ApplicationException($"Invalid setting: {setting}");
                }
            }
            catch (FormatException ex)
            {
                throw new ApplicationException($"{value} is an invalid value for {setting}");
            }
        }
    }
}
