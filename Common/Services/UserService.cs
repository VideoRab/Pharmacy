using Common.Mappers;
using Common.Services.Interfaces;
using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Common.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly UserMapper _mapper;

        public UserService(UserManager<User> userManager, UserMapper userMapper)
        {
            _userManager = userManager;
            _mapper = userMapper;
        }

        public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(user => user.Orders)!
                .ThenInclude(order => order.Medicines)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
            if (user is null)
            {
                throw new NullReferenceException($"User with id {id} was not found");
            }

            return _mapper.ReverseMap(user);
        }

        public async Task<IdentityResult> SignUpAsync(string username, string email, string password)
        {
            var user = new User
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true
            };

            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateAsync(Guid id, string username, string email, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(user => user.Orders)!
                .ThenInclude(order => order.Medicines)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
            if (user is null)
            {
                throw new NullReferenceException($"User with id {id} was not found");
            }

            user.UserName = username;
            user.Email = email;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(Guid id, string newPassword, string oldPassword, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(user => user.Orders)!
                .ThenInclude(order => order.Medicines)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
            if (user is null)
            {
                throw new NullReferenceException($"User with id {id} was not found");
            }

            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(user => user.Orders)!
                .ThenInclude(order => order.Medicines)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
            if (user is null)
            {
                throw new NullReferenceException($"User with id {id} was not found");
            }

            return await _userManager.DeleteAsync(user);
        }
    }
}
