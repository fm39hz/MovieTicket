namespace MovieTicket.Domain.Entity.Theater;

using System.ComponentModel.DataAnnotations;
using Movie;

public sealed record ShowtimeModel : BaseModel {
	public ShowtimeModel(ShowtimeModel showtime) : base(showtime) {
		MovieId = showtime.MovieId;
		ScreenId = showtime.ScreenId;
		TheaterId = showtime.TheaterId;
		StartTime = showtime.StartTime;
		EndTime = showtime.EndTime;
		Date = showtime.Date;
		TicketPrice = showtime.TicketPrice;
		AvailableSeats = showtime.AvailableSeats;
		BookedSeats = showtime.BookedSeats;
		Status = showtime.Status;
	}

	[Required]
	public Guid MovieId { get; init; }

	[Required]
	public Guid ScreenId { get; init; }

	[Required]
	public Guid TheaterId { get; init; }

	[Required]
	public TimeOnly StartTime { get; init; }

	[Required]
	public TimeOnly EndTime { get; init; }

	[Required]
	public DateOnly Date { get; init; }

	[Range(0.01, 999.99)]
	public decimal TicketPrice { get; init; }

	public int AvailableSeats { get; init; }

	public string BookedSeats { get; init; } = string.Empty;

	[Required]
	public string Status { get; init; } = "Active";

	public MovieModel? Movie { get; init; }
	public ScreenModel? Screen { get; init; }
	public TheaterModel? Theater { get; init; }
}
