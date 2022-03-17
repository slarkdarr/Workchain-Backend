namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using IF3250_2022_24_APPTS_Backend.Authorization;
using IF3250_2022_24_APPTS_Backend.Helpers;
using IF3250_2022_24_APPTS_Backend.Models.User;
using IF3250_2022_24_APPTS_Backend.Services;
using IF3250_2022_24_APPTS_Backend.Entities;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UserController(
        IUserService userService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    /// <summary>Logging in</summary>
    /// <returns>Message and Token</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///         "email": "amazon@gmail.com",
    ///         "password" : "password",
    ///         "type": "company"
    ///      }
    ///      
    /// </remarks>
    
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _userService.Authenticate(model);
        return Ok(response);
    }

    /// <summary>Register a User</summary>
    /// <returns>Message</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///         "email": "amazon@gmail.com",
    ///         "password" : "password",
    ///         "full_name" : "Amazon Web Service",
    ///         "phone_number" : "12345678",
    ///         "type": "company"
    ///      }
    ///      
    /// </remarks>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        await _userService.Register(model);
        return Ok(new { message = "Registration successful" });
    }

    /// <summary>Fetch list of Users</summary>
    /// <returns>List of all users</returns>
    /// <remarks>
    /// Requires Bearer Token in Header
    /// </remarks>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    /// <summary>Fetch User by user_id</summary>
    /// <returns>A User</returns>
    /// <remarks>
    /// Requires Bearer Token in Header
    /// </remarks>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetOne(id);
        return Ok(user);
    }

    /// <summary>Update User</summary>
    /// <returns>Message</returns>
    /// <remarks>
    /// Requires Bearer Token in Header
    /// 
    /// Sample request:
    ///
    ///     {
    ///       "full_name": "string",
    ///       "profile_picture": "string",
    ///       "birthdate": "string",
    ///       "phone_number": "string",
    ///       "gender": "string",
    ///       "country": "string",
    ///       "city": "string",
    ///       "headline": "string",
    ///       "description": "string",
    ///       "status": "string",
    ///       "type": "string"
    ///     }
    ///      
    /// </remarks>
    [HttpPut]
    public async Task<IActionResult> Update(UpdateRequest model)
    {
        var user = (User)HttpContext.Items["User"];
        await _userService.Update(user.user_id, model);
        return Ok(new { message = "User updated successfully" });
    }

    /// <summary>Delete a User by user_id</summary>
    /// <returns>Message</returns>
    /// <remarks>
    /// Requires Bearer Token in Header
    /// </remarks>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}