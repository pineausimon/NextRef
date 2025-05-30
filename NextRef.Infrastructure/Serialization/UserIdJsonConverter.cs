using NextRef.Domain.Core.Ids;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NextRef.Infrastructure.Serialization;
public class UserIdJsonConverter : JsonConverter<UserId>
{
    public override UserId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return new UserId(guid);
    }

    public override void Write(Utf8JsonWriter writer, UserId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}