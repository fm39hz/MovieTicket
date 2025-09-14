namespace MovieTicket.Application.Dto.Theater;

using Domain.Entity.Theater;

public record ScreenRequestDto(
	Guid TheaterId,
	int ScreenNumber,
	string ScreenType,
	int TotalSeats,
	string AudioSystem,
	string SeatLayout
) : IRequestDto<ScreenModel> {
	public ScreenModel ToModel() => new() {
		TheaterId = TheaterId,
		ScreenNumber = ScreenNumber,
		ScreenType = ScreenType,
		TotalSeats = TotalSeats,
		AudioSystem = AudioSystem,
		SeatLayout = SeatLayout
	};
}