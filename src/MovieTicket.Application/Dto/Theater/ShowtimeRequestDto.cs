namespace MovieTicket.Application.Dto.Theater;

using Domain.Entity.Theater;

public record ShowtimeRequestDto(
	Guid MovieId,
	Guid ScreenId,
	Guid TheaterId,
	DateTime StartTime,
	DateTime EndTime,
	DateTime Date,
	decimal TicketPrice,
	int AvailableSeats,
	List<int> BookedSeatsList,
	string Status
) : IRequestDto<ShowtimeModel> {
	public ShowtimeModel ToModel() => new() {
		MovieId = MovieId,
		ScreenId = ScreenId,
		TheaterId = TheaterId,
		StartTime = StartTime,
		EndTime = EndTime,
		Date = Date,
		TicketPrice = TicketPrice,
		AvailableSeats = AvailableSeats,
		BookedSeatsList = BookedSeatsList,
		Status = Status
	};
}