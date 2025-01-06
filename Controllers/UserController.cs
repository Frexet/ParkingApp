using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] User user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.UserId) || string.IsNullOrWhiteSpace(user.CarId))
            return BadRequest(new { message = "User ID and Car ID are required." });

        bool success = _userService.RegisterUser(user.UserId, user.CarId);
        if (!success)
            return Conflict(new { message = "User ID or Car ID is already registered." });

        return Ok(new { message = "User registered successfully.", userId = user.UserId, carId = user.CarId });
    }

    // Retrieve user details
    [HttpGet("{userId}")]
    public IActionResult GetUserDetails(string userId)
    {
        var user = _userService.GetUserDetails(userId);
        if (user == null) return NotFound(new { message = "User not found.", userId });

        return Ok(user);
    }

    // Retrieve the user's total parking cost
    [HttpGet("{userId}/cost")]
    public IActionResult GetUserCost(string userId)
    {
        double cost = _userService.GetUserCost(userId);
        if (cost == -1)
            return NotFound(new { message = "User not found or has no recorded parking sessions.", userId });

        return Ok(new { userId, totalCost = cost });
    }

    [HttpPost("{userId}/charge")]
    public IActionResult ChargeUser(string userId)
    {
        bool success = _userService.ChargeUser(userId);
        if (!success) return NotFound(new { message = "User not found.", userId });

        return Ok(new { message = "User has been charged, balance reset.", userId });
    }
}