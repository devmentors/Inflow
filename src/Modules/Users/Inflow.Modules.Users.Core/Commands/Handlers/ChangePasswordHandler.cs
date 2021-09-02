using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Inflow.Modules.Users.Core.Entities;
using Inflow.Modules.Users.Core.Exceptions;
using Inflow.Modules.Users.Core.Repositories;
using Inflow.Shared.Abstractions;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Users.Core.Commands.Handlers
{
    internal sealed class ChangePasswordHandler : ICommandHandler<ChangePassword>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<ChangePasswordHandler> _logger;

        public ChangePasswordHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
            ILogger<ChangePasswordHandler> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task HandleAsync(ChangePassword command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                .NotNull(() => new UserNotFoundException(command.UserId));
            
            if (_passwordHasher.VerifyHashedPassword(default, user.Password, command.CurrentPassword) ==
                PasswordVerificationResult.Failed)
            {
                throw new InvalidPasswordException("current password is invalid");
            }

            user.Password = _passwordHasher.HashPassword(default, command.NewPassword);;
            await _userRepository.UpdateAsync(user);
            _logger.LogInformation($"User with ID: '{user.Id}' has changed password.");
        }
    }
}