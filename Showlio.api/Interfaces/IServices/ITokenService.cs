using Showlio.api.Identity;

namespace Showlio.api.Interfaces.IService
{
    public interface ITokenService
    {
        public Task<string> CreateTokenAsync(AppUser appUser);
    }
}
