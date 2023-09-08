using Organizer.Lib.Common.Requests;
using Organizer.Lib.Common.Responses;

namespace Organizer.Lib.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> Login(LoginRequest request);
    Task Logout();
}