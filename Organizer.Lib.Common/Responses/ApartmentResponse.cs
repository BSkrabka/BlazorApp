namespace Organizer.Lib.Common.Responses;

public class ApartmentResponse
{
    public ApartmentResponse(Guid id, string name)
    {
        Name = name;
        Id = id;
    }

    public ApartmentResponse()
    {
        
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}