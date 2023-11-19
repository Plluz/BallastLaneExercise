using Assignment.Domain.Entities;

namespace Assignment.Api.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IUserService _userService;

    public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] string username, string password)
    {
        try
        {
            var user = await _userService.LoginAsync(username, password);

            if (user is null)
                return BadRequest();

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal error message here");
            return StatusCode(500, "An error occurred while signing in.");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        try
        {
            var created = await _userService.RegisterAsync(user);

            if (!created)
                return BadRequest();

            return Created();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal error message here");
            return StatusCode(500, "An error occurred while signing up.");
        }
    }
}
