using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServicesAbstrations;
using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //check if email exists
            var user =await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            //check if password 
            var IsPasswordValid =await _userManager.CheckPasswordAsync(user , loginDto.Password);


            if (IsPasswordValid)
            {
                //return UserDto
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = CreateTokenAsync(user)
                };
            }
            else
            {
                throw new  UnAuthorizedException();
            }

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // Mapping RegisterDto to ApplicationUser
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };

            //Create User [ApplicationUser]
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                //Return UserDto
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = CreateTokenAsync(user)
                };
            }
            else
            {
                
                //Throw Exception BadRequest
                var errors = result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(errors);
            }



        }

        private string CreateTokenAsync(ApplicationUser user)
        {
            return "TODO";
        }

    }
}
