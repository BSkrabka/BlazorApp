using Organizer.Lib.Common.Requests;
using Organizer.Lib.Common.Responses;

namespace Organizer.Lib.Core.Interfaces;

public interface ILoanService
{
    Task<OperationResult> Create(LoanCreateRequest request);
    Task<OperationResult> Remove(Guid apartmentId);
    Task<List<LoanResponse>> GetAll();
    Task<LoanResponse> GetById(Guid apartmentId);
}