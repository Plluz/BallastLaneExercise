using Assignment.Api.Input.Queries;

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
    public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
    {
        try
        {
            var token = await _userService.LoginAsync(loginQuery.Username, loginQuery.Password);

            if (string.IsNullOrWhiteSpace(token))
                return BadRequest();

            return Ok(token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal error message here");
            return StatusCode(500, "An error occurred while signing in.");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterQuery registerQuery)
    {
        try
        {
            var created = await _userService.RegisterAsync(registerQuery.Username, registerQuery.Password, registerQuery.PasswordConfirmation);

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
