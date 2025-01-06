public class ParkingRepository
{
    private readonly List<ParkingSession> _sessions = new();

    public void AddSession(ParkingSession session)
    {
        _sessions.Add(session);
    }

    public ParkingSession? GetActiveSession(string carId)
    {
        return _sessions.Find(s => s.CarId == carId && s.EndTime == null);
    }
}