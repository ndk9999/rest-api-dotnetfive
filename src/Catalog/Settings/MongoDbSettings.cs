namespace Catalog.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }


        public override string ToString()
        {
            return $"mongodb://{User}:{Password}@{Host}:{Port}";
        }
    }
}