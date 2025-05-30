using System.Text.Json;

namespace NextRef.Infrastructure.Serialization;
public static class JsonConvertersExtensions
{
    public static JsonSerializerOptions AddStronglyTypedIdConverters(this JsonSerializerOptions options)
    {
        options.Converters.Add(new UserIdJsonConverter());
        options.Converters.Add(new ContentIdJsonConverter());
        options.Converters.Add(new ContentMentionIdJsonConverter());
        options.Converters.Add(new ContributionIdJsonConverter());
        options.Converters.Add(new ContributorIdJsonConverter());
        options.Converters.Add(new UserCollectionIdJsonConverter());
        options.Converters.Add(new UserCollectionItemIdJsonConverter());
        return options; 
    }
}