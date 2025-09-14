namespace MovieTicket.Domain.Entity.Theater;

using System.ComponentModel.DataAnnotations;

public sealed record ScreenModel : BaseModel {
	public ScreenModel(ScreenModel screen) : base(screen) {
		TheaterId = screen.TheaterId;
		ScreenNumber = screen.ScreenNumber;
		ScreenType = screen.ScreenType;
		TotalSeats = screen.TotalSeats;
		AudioSystem = screen.AudioSystem;
		SeatLayout = screen.SeatLayout;
	}

	[Required]
	public Guid TheaterId { get; init; }

	[Required]
	public int ScreenNumber { get; init; }

	public string ScreenType { get; init; } = string.Empty;

	public int TotalSeats { get; init; }

	public string AudioSystem { get; init; } = string.Empty;

	public string SeatLayout { get; init; } = string.Empty;

	public TheaterModel? Theater { get; init; }
}
