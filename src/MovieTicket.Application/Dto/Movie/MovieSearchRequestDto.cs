namespace MovieTicket.Application.Dto.Movie;

public record MovieSearchRequestDto {
	public string? Title { get; set; }
	public string? Genre { get; set; }
	public decimal? MinRating { get; set; }
}
