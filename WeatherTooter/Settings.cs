using Newtonsoft.Json;

namespace WeatherTooter
{
    internal class Settings
    {
        private readonly string _settingsFileName = "settings.json";
        
        public float LocationLatitude { get; set; }
        public float LocationLongitude { get; set; }
        public string LocationName { get; set; }
        public string IanaTimeZoneName { get; set; }
        public byte HoursToForecast { get; set; }
        public string MastodonToken { get; set; }
        public string MastodonInstanceUrl { get; set; }
        public string TemplateFile { get; set; }

        public Settings(string settingsFileName)
        {
            _settingsFileName = settingsFileName;

            LocationLatitude = float.MinValue;
            LocationLongitude = float.MinValue;
            LocationName = string.Empty;
            IanaTimeZoneName = string.Empty;
            MastodonToken = string.Empty;
            MastodonInstanceUrl = string.Empty;
            TemplateFile = "toot-template.txt";
        }

        public static Settings Load(string fileName)
        {
            if (!File.Exists(fileName))
                return new Settings(fileName);

            var text = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Settings>(text) ??
                   throw new ApplicationException($"Your '{fileName}' appears to be empty or corrupt.");
        }

        public void Save()
        {
            var serialised = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(_settingsFileName, serialised);
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
                    case "ianatimezonename":
                        IanaTimeZoneName = value;
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
                    case "templatefile":
                        TemplateFile = value;
                        break;
                    default:
                        throw new ApplicationException($"Invalid setting: {setting}");
                }
            }
            catch (FormatException)
            {
                throw new ApplicationException($"{value} is an invalid value for {setting}");
            }
        }
    }
}
