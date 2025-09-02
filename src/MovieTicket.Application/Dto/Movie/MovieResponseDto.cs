namespace MovieTicket.Application.Dto.Movie;

using Common;
using Domain.Entity.Movie;

public class MovieResponseDto(MovieModel movie) : IResponseDto {
	public Guid Id { get; init; } = movie.Id;
	public string Title { get; init; } = movie.Title;
	public string Description { get; init; } = movie.Description;
	public int Duration { get; init; } = movie.Duration;
	public string Genre { get; init; } = movie.Genre;
	public string Director { get; init; } = movie.Director;
	public string Cast { get; init; } = movie.Cast;
	public DateTime ReleaseDate { get; init; } = movie.ReleaseDate;
	public string PosterUrl { get; init; } = movie.PosterUrl;
	public string TrailerUrl { get; init; } = movie.TrailerUrl;
	public decimal Rating { get; init; } = movie.Rating;
	public string Language { get; init; } = movie.Language;
}