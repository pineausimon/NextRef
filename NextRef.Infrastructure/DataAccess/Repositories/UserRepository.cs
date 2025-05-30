using NextRef.Domain.Users;
using NextRef.Infrastructure.DataAccess.Entities;
using Dapper;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(UserId id)
    {
        using var connection = _context.CreateConnection();
        const string query = "SELECT Id, UserName, Email FROM Core.Users WHERE Id = @Id";
        var entity = await connection.QuerySingleOrDefaultAsync<UserEntity>(query, new { Id = id });

        return UserMapper.ToDomain(entity);
    }

    public async Task AddAsync(User user)
    {
        const string query = @"
            INSERT INTO Core.Users (Id, UserName, Email, CreatedAt, UpdatedAt)
            VALUES (@Id, @UserName, @Email, @CreatedAt, @UpdatedAt)";

        using var connection = _context.CreateConnection();
        var added = await connection.ExecuteAsync(query, new
        {
            user.Id,
            user.UserName,
            user.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        if (added == 0)
            throw new InvalidDataException();
    }

    public async Task UpdateAsync(User user)
    {
        const string query = "UPDATE Core.Users SET Email = @Email, UserName = @UserName, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        using var connection = _context.CreateConnection();

        var updated = await connection.ExecuteAsync(query, new
        {
            user.UserName,
            user.Email,
            UpdatedAt = DateTime.UtcNow,
            user.Id
        });

        if (updated == 0)
            throw new InvalidDataException();
    }

    public async Task DeleteAsync(UserId id)
    {
        const string query = "DELETE FROM Core.Users WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}
