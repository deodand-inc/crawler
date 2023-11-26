using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace crawler.scripts.utils;

public class YamlParser
{
    
    private static YamlParser _instance = null;

    private IDeserializer _deserializer;

    public static YamlParser Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new YamlParser();
            }
            return _instance;
        }
    }

    private YamlParser()
    {
        _deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
    }

    public T Parse<T>(string data)
    {
        return _deserializer.Deserialize<T>(data);
    }
}