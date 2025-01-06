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
            return BadRequest(new { message = "Invalid input. User ID and Car ID are required." });

        _userService.RegisterUser(user.UserId, user.CarId);
        return Ok(new { message = "User registered successfully.", userId = user.UserId, carId = user.CarId });
    }

    // Hämta användardetaljer
    [HttpGet("{userId}")]
    public IActionResult GetUserDetails(string userId)
    {
        var user = _userService.GetUserDetails(userId);
        if (user == null) return NotFound(new { message = "User not found.", userId });

        return Ok(user);
    }

    // Hämta användarens totala parkeringskostnad
    [HttpGet("{userId}/cost")]
    public IActionResult GetUserCost(string userId)
    {
        double totalCost = _userService.GetUserCost(userId);
        return Ok(new { userId, totalCost });
    }
}