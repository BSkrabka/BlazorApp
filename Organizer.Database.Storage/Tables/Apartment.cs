namespace Organizer.Database.Storage.Tables;

public class Apartment : BaseEntity
{
    public required string Name { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
    public int? ApartmentNumber { get; set; }
    public string ZipCode { get; set; }
}