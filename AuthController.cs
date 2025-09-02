[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto userDto)
    {
        var user = await _authService.RegisterAsync(userDto);
        if (user == null)
            return BadRequest("Registration failed");
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto userDto)
    {
        var result = await _authService.LoginAsync(userDto);
        if (result == null)
            return BadRequest("Invalid username or password");
        return Ok(result);
    }
}
