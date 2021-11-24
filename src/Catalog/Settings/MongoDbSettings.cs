namespace Catalog.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }


        public override string ToString()
        {
            return $"mongodb://{Host}:{Port}";
        }
    }
}