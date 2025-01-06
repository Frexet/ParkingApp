public class UserRepository
{
    private readonly List<User> _users = new();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User? GetUser(string userId)
    {
        return _users.Find(u => u.UserId == userId);
    }
}