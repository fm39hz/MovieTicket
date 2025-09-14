namespace MovieTicket.Application.Dto.Theater;

using Domain.Entity.Theater;

public record TheaterRequestDto(
	string Name,
	string Address,
	string City,
	string State,
	string ZipCode,
	string PhoneNumber,
	int ScreenCount,
	bool ParkingAvailable,
	string Facilities
) : IRequestDto<TheaterModel> {
	public TheaterModel ToModel() => new() {
		Name = Name,
		Address = Address,
		City = City,
		State = State,
		ZipCode = ZipCode,
		PhoneNumber = PhoneNumber,
		ScreenCount = ScreenCount,
		ParkingAvailable = ParkingAvailable,
		Facilities = Facilities
	};
}