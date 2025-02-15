public class UserService
{
    private readonly List<User> _users = new();

    // Registers a new user with their car.
    public bool RegisterUser(string userId, string carId)
    {
        if (_users.Exists(u => u.UserId == userId || u.CarId == carId))
            return false; // Prevent duplicate users or cars

        _users.Add(new User { UserId = userId, CarId = carId, TotalCost = 0 });
        return true;
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
        if (user == null)
            return -1; // Indicate user not found

        return user.TotalCost;
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

    public bool ChargeUser(string userId)
    {
        var user = _users.Find(u => u.UserId == userId);
        if (user == null) return false; // User not found

        user.TotalCost = 0;  // Resets the total cost after the user has paid
        user.HasPaid = true;  // Marks the user as paid

        return true; // Success
    }


}