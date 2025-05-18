using Microsoft.AspNetCore.Mvc;
using ServicesAbstrations;
using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contollers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager _serviceManager) :ControllerBase
    {

        //Login
        [HttpPost("Login")] //POST BasuUrl/api/Authentication/Login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        }
        //Register
        [HttpPost("Register")] //POST BasuUrl/api/Authentication/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user =await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }

    }
}
