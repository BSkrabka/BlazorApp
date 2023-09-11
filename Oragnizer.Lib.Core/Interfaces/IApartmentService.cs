using Organizer.Lib.Common.Requests;
using Organizer.Lib.Common.Responses;

namespace Organizer.Lib.Core.Interfaces;

public interface IApartmentService
{
    Task<OperationResult> Create(ApartmentCreateRequest request);
    Task<OperationResult> Remove(Guid apartmentId);
    Task<List<ApartmentResponse>> GetAll();
    Task<ApartmentResponse> GetById(Guid apartmentId);
}