namespace NextRef.Infrastructure.DataAccess.Helper;
public static class ContentRowVersionCache
{
    private static readonly Dictionary<Guid, byte[]> _rowVersions = new();

    public static void Set(Guid id, byte[] rowVersion)
        => _rowVersions[id] = rowVersion;

    public static byte[]? Get(Guid id)
        => _rowVersions.TryGetValue(id, out var version) ? version : null;
}
