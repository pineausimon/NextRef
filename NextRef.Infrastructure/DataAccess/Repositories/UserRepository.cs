using NextRef.Domain.Users;
using NextRef.Infrastructure.DataAccess.Entities;
using Dapper;
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

    public async Task<User?> GetByIdAsync(Guid id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT Id, UserName, Email FROM Users WHERE Id = @Id";
        var entity = await connection.QuerySingleOrDefaultAsync<UserEntity>(sql, new { Id = id });

        return UserMapper.ToDomain(entity);
    }

    public async Task AddAsync(User user)
    {
        var sql = @"
            INSERT INTO USERS (Id, UserName, Email, CreatedAt, UpdatedAt)
            VALUES (@Id, @UserName, @Email, @CreatedAt, @UpdatedAt)";

        using var connection = _context.CreateConnection();
        var added = await connection.ExecuteAsync(sql, new
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
        var sql = "UPDATE Users SET Email = @Email, UserName = @UserName, UpdatedAt = @UpdatedAt WHERE Id = @Id";
        using var connection = _context.CreateConnection();

        var updated = await connection.ExecuteAsync(sql, new
        {
            user.UserName,
            user.Email,
            UpdatedAt = DateTime.UtcNow,
            user.Id
        });

        if (updated == 0)
            throw new InvalidDataException();
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = "DELETE FROM User WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
