namespace ChangeTracker
{
    using System.Text.Json;

    public class Config
    {
        public string consumerId { get; set; }
        public string[] fields { get; set; }
    }
}
