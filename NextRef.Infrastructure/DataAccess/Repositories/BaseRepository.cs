using Dapper;
using NextRef.Infrastructure.DataAccess.Configuration;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public abstract class BaseRepository<TEntity, TId>
{
    protected readonly DapperContext _context;
    protected BaseRepository(DapperContext context) => _context = context;

    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();
        var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);
        return await connection.QueryAsync<T>(command);
    }

    protected async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? parameters, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();
        var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<T>(command);
    }

    protected async Task<int> ExecuteAsync(string sql, object? parameters, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();
        var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);
        return await connection.ExecuteAsync(command);
    }
}