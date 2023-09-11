using Organizer.Database.Storage.Repositories.Interfaces;
using Organizer.Database.Storage.Tables;
using Organizer.Lib.Common.Requests;
using Organizer.Lib.Common.Responses;
using Organizer.Lib.Core.Interfaces;

namespace Organizer.Lib.Core.Services;

public class ApartmentService : IApartmentService
{
    private readonly IApartmentRepository _apartmentRepository;

    public ApartmentService(IApartmentRepository apartmentRepository)
    {
        _apartmentRepository = apartmentRepository;
    }

    public async Task<OperationResult> Create(ApartmentCreateRequest request)
    {
        var apartment = new Apartment()
        {
            Name = request.Name
        };

        await _apartmentRepository.CreateAsync(apartment);

        return OperationResult.Ok();
    }

    public async Task<OperationResult> Remove(Guid apartmentId)
    {
        var apartment = await _apartmentRepository.GetAsync(a => a.Id == apartmentId);

        if (apartment == null)
        {
            return OperationResult.Failure("Nie znaleziono mieszkania");
        }

        await _apartmentRepository.DeleteAsync(apartment);

        return OperationResult.Ok();
    }

    public async Task<List<ApartmentResponse>> GetAll()
    {
        var apartments = await _apartmentRepository.GetAllAsync();

        return apartments.Select(a => new ApartmentResponse(a.Id, a.Name)).ToList();
    }

    public async Task<ApartmentResponse> GetById(Guid apartmentId)
    {
        var apartment = await _apartmentRepository.GetAsync(a => a.Id == apartmentId);

        if (apartment == null) return new ApartmentResponse();

        return new ApartmentResponse(apartment.Id, apartment.Name);
    }
}