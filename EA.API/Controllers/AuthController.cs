using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
   private readonly IAuthService _authService;
   private readonly IAppUserService _userService;

   public AuthController(IAuthService authService,IAppUserService userService)
    {
        _authService=authService;
        _userService=userService;
    } 

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateAppUserDto userDto)
    {
        await _userService.CreateAsync(userDto);
        return Ok("Kullanici Basari İle Olusturuldu");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var complete=await _authService.LoginAsync(loginDto);
        return Ok(complete);
    }
}
