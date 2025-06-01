using System.Text.Json;

namespace NextRef.Infrastructure.Serialization;
public static class JsonOptionsFactory
{
    public static JsonSerializerOptions Create()
    {
        var options = new JsonSerializerOptions();
        options.AddStronglyTypedIdConverters();
        return options;
    }
}