using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using storeApp.domain.dtos;


[ApiVersion("1.0")]
[Route("api/v{version:ApiVersion}/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;


    public UserController(UserService userService)
    {
        _userService = userService;

    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll() => Ok(await _userService.GetAllUsers());

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);

        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost("signup")]

    public async Task<IActionResult> UserSignup(RegisterDto user)
    {

        await _userService.AddUser(user);

        return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
    }

    [HttpPost("signin")]

    public async Task<AuthResponseDto> UserSignin(LoginDto request, IConfiguration config)
    {

        var token = await _userService.UserLogin(request.email, request.password, config);

        if (token.Token == null)
        {
            return new AuthResponseDto { Success = false, Message = token.Message };
        }


        return new AuthResponseDto { Success = true, Token = token.Token };
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {

        return await _userService.DeleteUser(id) ? NoContent() : NotFound();
    }


}