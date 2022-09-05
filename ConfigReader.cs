using System.Text.Json;

namespace ChangeTracker
{
    public class ConfigReader
    {
        public static Config[] ReadConfig()
        {
            using (StreamReader r = new StreamReader("config.json"))
            {
                string json = r.ReadToEnd();
                return JsonSerializer.Deserialize<Config[]>(json) ?? Array.Empty<Config>();
            }
        }

    }
}
