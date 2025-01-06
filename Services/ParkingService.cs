public class ParkingService
{
    private readonly List<ParkingSession> _sessions = new(); // Tracks active and past parking sessions

    // Starts a new parking session for a car
    public void StartParking(string carId)
    {
        _sessions.Add(new ParkingSession { CarId = carId, StartTime = DateTime.Now });
    }

    // Ends an active parking session and calculates the cost
    public double EndParking(string carId)
    {
        var session = _sessions.Find(s => s.CarId == carId && s.EndTime == null);
        if (session == null)
            return -1; // No active session found

        session.EndTime = DateTime.Now;
        session.Cost = CalculateCost(session.StartTime, session.EndTime.Value);

        return session.Cost;
    }

    // Gets the current active parking session for a car
    public ParkingSession? GetCurrentParking(string carId)
    {
        return _sessions.Find(s => s.CarId == carId && s.EndTime == null);
    }

    // Calculates the parking cost based on time and hourly rates
    private double CalculateCost(DateTime start, DateTime end)
    {
        double totalCost = 0;
        DateTime currentTime = start;

        while (currentTime < end)
        {
            int hour = currentTime.Hour;
            double hourlyRate = (hour >= 8 && hour < 18) ? 14 : 6;
            totalCost += hourlyRate;
            currentTime = currentTime.AddHours(1);
        }

        return totalCost;
    }
}