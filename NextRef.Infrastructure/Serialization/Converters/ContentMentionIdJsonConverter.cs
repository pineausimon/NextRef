using System.Text.Json.Serialization;
using System.Text.Json;
using NextRef.Domain.Core.Ids;

namespace NextRef.Infrastructure.Serialization;
public class ContentMentionIdJsonConverter : JsonConverter<ContentMentionId>
{
    public override ContentMentionId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return new ContentMentionId(guid);
    }

    public override void Write(Utf8JsonWriter writer, ContentMentionId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
