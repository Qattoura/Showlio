using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.IdentityModel.Tokens;
using Showlio.api.Dtos.Account;
using Showlio.api.Identity;
using Showlio.api.Interfaces.IService;
using Showlio.api.Interfaces.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Showlio.api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ICurrentUserService _currentUserService;

        public AccountController(UserManager<AppUser> userManager,
            ITokenService tokenService, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _currentUserService = currentUserService;

        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto) 
        {
            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {

                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };
                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (!createUser.Succeeded)
                {
                    return StatusCode(500, createUser.Errors);

                }

                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                if (roleResult.Succeeded)
                {

                    return Ok(

                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Id = appUser.Id,
                                Token = await _tokenService.CreateTokenAsync(appUser)
                            }
                            );
                }

                return StatusCode(500, roleResult.Errors);


            }

            catch (Exception e)
            {
                return StatusCode(500, e);

            }

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await _userManager.FindByNameAsync(loginDto.UserName);

            if (appUser == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(
                appUser,
                loginDto.Password);

            if (!passwordValid)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok( new NewUserDto 
            {
                UserName = appUser.UserName,
                Id = appUser.Id,
                Token = await _tokenService.CreateTokenAsync(appUser)
            });
        }

  

        //[Authorize(Roles = "user")]
        [Authorize]
        [HttpGet("test-auth")]
        public IActionResult TestAuth()
        {
            var userId = _currentUserService.UserId;
            return Ok(userId);
        }

    }
}
