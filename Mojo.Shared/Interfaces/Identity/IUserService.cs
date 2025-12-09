using System.Security.Claims;
using Mojo.Shared.Dtos.Identity;

namespace Mojo.Shared.Interfaces.Identity;

public interface IUserService
{
    Task<ApplicationUserDto?> GetUserAsync(ClaimsPrincipal principal, CancellationToken ct = default);
}