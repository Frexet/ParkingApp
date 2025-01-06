public class ParkingSession
{
    public required string CarId { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; }
    public double Cost { get; set; } = 0;
}
