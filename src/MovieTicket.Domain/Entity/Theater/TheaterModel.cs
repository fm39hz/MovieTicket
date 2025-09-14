namespace MovieTicket.Domain.Entity.Theater;

using System.ComponentModel.DataAnnotations;

public sealed record TheaterModel : BaseModel {
	public TheaterModel(TheaterModel theater) : base(theater) {
		Name = theater.Name;
		Address = theater.Address;
		City = theater.City;
		State = theater.State;
		ZipCode = theater.ZipCode;
		PhoneNumber = theater.PhoneNumber;
		ScreenCount = theater.ScreenCount;
		ParkingAvailable = theater.ParkingAvailable;
		Facilities = theater.Facilities;
	}

	[Required]
	public string Name { get; init; } = string.Empty;

	[Required]
	public string Address { get; init; } = string.Empty;

	[Required]
	public string City { get; init; } = string.Empty;

	[Required]
	public string State { get; init; } = string.Empty;

	public string ZipCode { get; init; } = string.Empty;

	public string PhoneNumber { get; init; } = string.Empty;

	public int ScreenCount { get; init; }

	public bool ParkingAvailable { get; init; }

	public string Facilities { get; init; } = string.Empty;
}
