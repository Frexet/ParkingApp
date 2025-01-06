using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/parking")]
public class ParkingController : ControllerBase
{
    private readonly ParkingService _parkingService;
    private readonly UserService _userService;

    public ParkingController(ParkingService parkingService, UserService userService)
    {
        _parkingService = parkingService ?? throw new ArgumentNullException(nameof(parkingService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    // Starts a new parking session
    [HttpPost("start")]
    public IActionResult StartParking([FromBody] CarRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.CarId))
            return BadRequest(new { message = "Car ID is required." });

        _parkingService.StartParking(request.CarId);
        return Ok(new { message = "Parking started.", carId = request.CarId });
    }

    // Ends the current parking session
[HttpPost("end")]
public IActionResult EndParking([FromBody] CarRequest request)
{
    if (request == null || string.IsNullOrWhiteSpace(request.CarId))
        return BadRequest(new { message = "Car ID is required." });

    double cost = _parkingService.EndParking(request.CarId);
    if (cost == -1)
        return NotFound(new { message = "No active parking session found for this car.", request.CarId });

    _userService.AddParkingCostToUser(request.CarId, cost);
    return Ok(new { message = "Parking ended.", request.CarId, totalCost = cost });
}

    // Retrieves the current active parking session for a car
    [HttpGet("current/{carId}")]
    public IActionResult GetCurrentParking(string carId)
    {
        if (string.IsNullOrWhiteSpace(carId))
            return BadRequest(new { message = "Car ID is required." });

        var session = _parkingService.GetCurrentParking(carId);
        if (session == null)
            return NotFound(new { message = "No active parking session.", carId });

        return Ok(session);
    }
}