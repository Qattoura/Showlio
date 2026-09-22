using Microsoft.AspNetCore.Identity;
using Showlio.api.Models;

namespace Showlio.api.Identity
{
    public class AppUser : IdentityUser<Guid>
    {

        public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    }
}
