using NextRef.Domain.Core.Ids;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NextRef.Infrastructure.Serialization;
public class UserCollectionIdJsonConverter : JsonConverter<UserCollectionId>
{
    public override UserCollectionId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return new UserCollectionId(guid);
    }

    public override void Write(Utf8JsonWriter writer, UserCollectionId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}