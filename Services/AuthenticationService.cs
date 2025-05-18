
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstrations;
using Shared.DataTransferObject.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration/*عشان اوصل ل appsetting*/) : IAuthenticationService
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
                    Token =await CreateTokenAsync(user)
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
                    Token =await CreateTokenAsync(user)
                };
            }
            else
            {
                
                //Throw Exception BadRequest
                var errors = result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(errors);
            }



        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.Email , user.Email!),
                new(ClaimTypes.Name , user.UserName!),
                new(ClaimTypes.NameIdentifier , user.Id!)
            };            
            var Roles =await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"]; //Get from appsettings
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credintials = new SigningCredentials(Key , SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(

                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credintials);

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

    }
}
