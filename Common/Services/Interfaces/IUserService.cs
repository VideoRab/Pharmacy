using Entities.DTO;
using Microsoft.AspNetCore.Identity;

namespace Common.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IdentityResult> SignUpAsync(string username, string email, string password);
    Task<IdentityResult> UpdateAsync(Guid id, string username, string email, CancellationToken cancellationToken);
    Task<IdentityResult> ChangePasswordAsync(Guid id, string newPassword, string oldPassword, CancellationToken cancellationToken);
    Task<IdentityResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
