using System.Text.Json.Serialization;
using System.Text.Json;
using NextRef.Domain.Core.Ids;

namespace NextRef.Infrastructure.Serialization;
public class ContributorIdJsonConverter : JsonConverter<ContributorId>
{
    public override ContributorId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return new ContributorId(guid);
    }

    public override void Write(Utf8JsonWriter writer, ContributorId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
