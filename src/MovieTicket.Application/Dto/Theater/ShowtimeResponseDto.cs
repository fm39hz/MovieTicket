namespace MovieTicket.Application.Dto.Theater;

using Common;
using Domain.Entity.Theater;

public class ShowtimeResponseDto(ShowtimeModel showtime) : IResponseDto {
	public Guid Id { get; init; } = showtime.Id;
	public Guid MovieId { get; init; } = showtime.MovieId;
	public Guid ScreenId { get; init; } = showtime.ScreenId;
	public Guid TheaterId { get; init; } = showtime.TheaterId;
	public DateTime StartTime { get; init; } = showtime.StartTime;
	public DateTime EndTime { get; init; } = showtime.EndTime;
	public DateTime Date { get; init; } = showtime.Date;
	public decimal TicketPrice { get; init; } = showtime.TicketPrice;
	public int AvailableSeats { get; init; } = showtime.AvailableSeats;
	public string BookedSeats { get; init; } = showtime.BookedSeats;
	public string Status { get; init; } = showtime.Status;
}