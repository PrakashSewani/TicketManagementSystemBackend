using Application.Features.UserAuthentication.Commands;
using Application.Features.UserAuthentication.Queries;
using Application.Models.UserAuthentication;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        public UserController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(RegisterUser Request)
        {
            try
            {
                LoggedInUser Response = await _mediatrSender.Send(new RegisterUserRequest(Request));
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUser Request)
        {
            try
            {
                User Response = await _mediatrSender.Send(new UpdateUserRequest(Request));
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(LoginUser Request)
        {
            try
            {
                LoggedInUser Response = await _mediatrSender.Send(new LoginUserRequest(Request));
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid UserGuid)
        {
            try
            {
                await _mediatrSender.Send(new DeleteUserRequest(UserGuid));
                return Ok("Operation Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var Identity = HttpContext.User.Identity as ClaimsIdentity;
                string GuidClaim = Identity.FindFirst("UserId").Value;
                return Ok(await _mediatrSender.Send(new GetUserRequest(GuidClaim)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-user-role")]
        [Authorize]
        public async Task<IActionResult> UpdateUserRole(UpdateUserRole Request)
        {
            try
            {
                var Identity = HttpContext.User.Identity as ClaimsIdentity;
                string GuidClaim = Identity.FindFirst("UserId").Value;
                User User = await _mediatrSender.Send(new UpdateUserRoleRequest(Request, Guid.Parse(GuidClaim)));
                return Ok(User);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
