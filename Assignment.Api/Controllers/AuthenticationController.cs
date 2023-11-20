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

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="loginQuery">The login object including username (string) and password (string).</param>
    /// <returns>Returns a token on successful login.</returns>
    /// <response code="200">Returns a token on successful login.</response>
    /// <response code="400">If there's an issue with the request or login fails.</response>
    /// <response code="500">If an unexpected error occurs.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
    {
        var token = await _userService.LoginAsync(loginQuery.Username, loginQuery.Password);

        if (string.IsNullOrWhiteSpace(token))
            return BadRequest();

        return Ok(token);
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerQuery">The registration information including username, password, and password confirmation.</param>
    /// <returns>Returns a response indicating success or failure.</returns>
    /// <response code="201">Indicates successful registration of a new user.</response>
    /// <response code="400">If there's an issue with the request or registration fails.</response>
    /// <response code="500">If an unexpected error occurs.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterQuery registerQuery)
    {
        var created = await _userService.RegisterAsync(registerQuery.Username, registerQuery.Password, registerQuery.PasswordConfirmation);

        if (!created)
            return BadRequest();

        return Created();
    }
}
