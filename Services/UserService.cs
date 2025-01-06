public class UserService
{
    private readonly List<User> _users = new(); 

    // Registers a new user with their car.
    public void RegisterUser(string userId, string carId)
    {
        _users.Add(new User { UserId = userId, CarId = carId, TotalCost = 0 });
    }

    // Retrieves user details by user ID.
    public User? GetUserDetails(string userId)
    {
        return _users.Find(u => u.UserId == userId);
    }

    // Gets the total parking cost for a user.
    public double GetUserCost(string userId)
    {
        var user = _users.Find(u => u.UserId == userId);
        return user?.TotalCost ?? 0;
    }

    // Adds parking cost to the user's total balance.
    public void AddParkingCostToUser(string carId, double cost)
    {
        var user = _users.Find(u => u.CarId == carId);
        if (user != null)
        {
            user.TotalCost += cost;
        }
    }
}