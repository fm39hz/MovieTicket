namespace MovieTicket.Application.Dto.Theater;

public record SeatAvailabilityDto {
	public int TotalSeats { get; init; }
	public int AvailableSeats { get; init; }
	public int BookedSeatsCount { get; init; }
	public List<string> BookedSeatsList { get; init; } = new();
}