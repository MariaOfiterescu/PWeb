using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pweb.API.Models.DTOs;
using Pweb.API.Repositories;

namespace Pweb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public Auth(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //POST : /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded) 
            {
                //Add roles to this user 

                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRoleAsync(identityUser, registerRequestDTO.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
                   
            }

            return BadRequest("Something went wrong!");

        }


        //POST : /api/Auth/Login

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByNameAsync(loginRequestDTO.UserName);

            if (user !=null)
            {
               var checkPasswordResult =  await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);// boolean

                if (checkPasswordResult)
                {
                    //create a JWT Token

                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //create token
                        //
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        
                        };

                         return Ok(response);
                    }

                }
            }

            return BadRequest("Username or password incorrect !!!");
        }
    }
}
