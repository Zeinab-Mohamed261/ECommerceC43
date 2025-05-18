using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstrations
{
    public interface IAuthenticationService
    {
        //Login
        //Take email and password
        //Then return token , Email , DisplayName
        Task<UserDto> LoginAsync(LoginDto loginDto);
        //Register
        //Take email , password , UserName And Phone Number
        //Then return token , Email , DisplayName
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
    }
}
