namespace MovieTicket.Application.Dto.Statistics;

public record StatisticsOverviewDto {
	public int TotalBookings { get; init; }
	public int TotalUsers { get; init; }
	public int TotalMovies { get; init; }
	public int TotalTheaters { get; init; }
	public decimal TotalRevenue { get; init; }
	public int TotalShowtimes { get; init; }
}