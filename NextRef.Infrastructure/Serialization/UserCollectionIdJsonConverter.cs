using NextRef.Domain.Core.Ids;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NextRef.Infrastructure.Serialization;
public class UserCollectionItemIdJsonConverter : JsonConverter<UserCollectionItemId>
{
    public override UserCollectionItemId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return new UserCollectionItemId(guid);
    }

    public override void Write(Utf8JsonWriter writer, UserCollectionItemId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}