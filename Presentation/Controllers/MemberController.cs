using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/member")]
    public class MemberController : ControllerBase
    {
        private readonly IServiceManager _services;

        public MemberController(IServiceManager services)
        {
            _services = services;
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterationDto userRegisterationDto)
        {
            var result = await _services.AuthenticationService.RegisterUser(userRegisterationDto);

            if (!result.Succeeded)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _services.AuthenticationService.ValidateUser(user))
                return Unauthorized();

            var tokenDto = await _services.AuthenticationService.CreateToken(true);
            return Ok(tokenDto);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> CreateRefreshToken([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _services.AuthenticationService.RefreshToken(tokenDto);
            return Ok(tokenDtoToReturn);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPass([FromBody]UserForAuthenticationDto userForAuthenticationDto)
        {
            await _services.AuthenticationService.ChangePassword(userForAuthenticationDto.Email,userForAuthenticationDto.Password);
            return Ok();
        }
    }
}
