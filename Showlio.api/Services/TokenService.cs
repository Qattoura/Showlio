using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Showlio.api.Identity;
using Showlio.api.Interfaces.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Showlio.api.Services
{
    public class TokenService : ITokenService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;

        public TokenService(
            UserManager<AppUser> userManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<string> CreateTokenAsync(AppUser appUser)
        {
            var roles = await _userManager.GetRolesAsync(appUser);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Name, appUser.UserName!)
        };

            claims.AddRange(
                roles.Select(role => new Claim(ClaimTypes.Role, role))
            );

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _config["JWT:Secret"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(_config["JWT:ExpirationInMinutes"]!)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
