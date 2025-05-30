using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Users;
public class User
{
    public UserId Id { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    private User(UserId id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    public static User Rehydrate(UserId id, string userName, string email)
        => new(id, userName, email);


    public static User CreateFromAppUser(Guid id, string userName, string email)
        => new(new UserId(id), userName, email);

    public void Update(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }
}
