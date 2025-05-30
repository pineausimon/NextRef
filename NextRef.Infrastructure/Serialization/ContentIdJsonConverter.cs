using System.Text.Json.Serialization;
using System.Text.Json;
using NextRef.Domain.Core.Ids;

namespace NextRef.Infrastructure.Serialization;
public class ContentIdJsonConverter : JsonConverter<ContentId>
{
    public override ContentId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return new ContentId(guid);
    }

    public override void Write(Utf8JsonWriter writer, ContentId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
