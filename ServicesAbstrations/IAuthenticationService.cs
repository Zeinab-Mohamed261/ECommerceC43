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

        //chech Email
        //Take Email Then return Bool Client
        Task<bool> CheckEmailAsync(string email);

        //Get Current User Address
        //Take Email Then return Address of current logged in user
        Task<AddressDto> GetCurrentUserAddressAsync(string email);

        //Update Current User Address
        //Take Updated Address & Email Then return Address after updated
        Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto addressDto, string email);

        //Get Current User
        //Take Email Then return Token , Email , DisplayName
        Task<UserDto> GetCurrentUserAsync(string email );
    }
}
