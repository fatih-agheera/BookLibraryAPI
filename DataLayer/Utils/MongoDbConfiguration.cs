namespace DataLayer.Utils;

public class MongoDbConfiguration
{
    public static string Section = "MongoDBConfiguration";
    public string ConnectionURI { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}