namespace MovieTicket.Application.Dto.Movie;

using Common;

public record MovieSearchRequestDto {
	public string? Title { get; set; }
	public string? Genre { get; set; }
	public decimal? MinRating { get; set; }
	public PaginationRequestDto? Pagination { get; set; }
}