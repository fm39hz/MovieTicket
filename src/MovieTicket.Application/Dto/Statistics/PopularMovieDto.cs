namespace MovieTicket.Application.Dto.Statistics;

public record PopularMovieDto {
	public Guid MovieId { get; init; }
	public string Title { get; init; } = string.Empty;
	public string Genre { get; init; } = string.Empty;
	public int BookingsCount { get; init; }
	public decimal TotalRevenue { get; init; }
	public decimal Rating { get; init; }
}