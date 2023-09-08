using Organizer.Lib.Common.Responses;

namespace Organizer.Lib.Core.Interfaces;

public interface IUserService
{
    Task<List<UserResponse>> GetUsersAsync();
    Task<OperationResult> RemoveUserAsync(Guid userId);
}