using Organizer.Database.Storage.Repositories.Interfaces;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;
using Organizer.Lib.Helper.Resources;

namespace Organizer.Lib.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponse>> GetUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(u => new UserResponse()
        { Id = u.Id, Email = u.Email, Login = u.Login, Name = u.Name, Surname = u.Surname })
            .ToList();
    }

    public async Task<OperationResult> RemoveUserAsync(Guid userId)
    {
        var user = await _userRepository.GetAsync(u => u.Id == userId);

        if (user == null) 
            return OperationResult.Failure(Messages.UserNotFound);

        await _userRepository.DeleteAsync(user);

        return OperationResult.Ok();
    }
}