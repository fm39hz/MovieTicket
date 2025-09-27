namespace MovieTicket.Application.Dto.Theater;

public record SeatAvailabilityDto {
	public int TotalSeats { get; init; }
	public int AvailableSeats { get; init; }
	public int BookedSeatsCount { get; init; }
	public List<int> BookedSeatsList { get; init; } = new();
	public List<int> AllSeatsList { get; init; } = new();
	public List<int> AvailableSeatsList { get; init; } = new();
	public string SeatLayout { get; init; } = string.Empty;
}