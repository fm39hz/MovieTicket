namespace MovieTicket.Application.Dto.Theater;

using Domain.Entity.Theater;

public record ShowtimeRequestDto(
	Guid MovieId,
	Guid ScreenId,
	Guid TheaterId,
	TimeOnly StartTime,
	TimeOnly EndTime,
	DateOnly Date,
	decimal TicketPrice,
	int AvailableSeats,
	string BookedSeats,
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
		BookedSeats = BookedSeats,
		Status = Status
	};
}