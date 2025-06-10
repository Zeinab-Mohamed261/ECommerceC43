using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstrations;
using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contollers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {

        //Login
        //[Authorize(Roles ="Admin")]
        [HttpPost("Login")] //POST BasuUrl/api/Authentication/Login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);
        }
        //Register
        [HttpPost("Register")] //POST BasuUrl/api/Authentication/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }
        //Check Email
        [HttpGet("CheckEmail")] //GET BasuUrl/api/Authentication/CheckEmail}
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }
        //Get Current User 
        [Authorize]
        [HttpGet("CurrentUser")] //GET BasuUrl/api/Authentication/CurrentUser
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(user);
        }
        //Get Current User Address
        [HttpGet("CurrentUserAddress")] //GET BasuUrl/api/Authentication/CurrentUserAddress
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email);
            return Ok(address);
        }

        //Update Current User Address
        [HttpPut("Address")] //PUT BasuUrl/api/Authentication/Address
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(addressDto, email);
            return Ok(updatedAddress);
        }
    }
}
