using System.Text.Json.Serialization;
using System.Text.Json;
using NextRef.Domain.Core.Ids;

namespace NextRef.Infrastructure.Serialization;
public class ContributionIdJsonConverter : JsonConverter<ContributionId>
{
    public override ContributionId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return new ContributionId(guid);
    }

    public override void Write(Utf8JsonWriter writer, ContributionId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
