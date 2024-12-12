using Application.Features.UserAuthentication.Commands;
using Application.Features.UserAuthentication.Queries;
using Application.Models.UserAuthentication;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
                User Response = await _mediatrSender.Send(new RegisterUserRequest(Request));
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        [AllowAnonymous]
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
        public async Task<IActionResult> DeleteUser(Guid Request)
        {
            try
            {
                await _mediatrSender.Send(new DeleteUserRequest(Request));
                return Ok("Operation Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
