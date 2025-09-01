namespace MovieTicket.Domain.Entity.Movie;

using System.ComponentModel.DataAnnotations;

public sealed record MovieModel : BaseModel {
	public MovieModel(MovieModel movie) : base(movie) {
		Title = movie.Title;
		Description = movie.Description;
		Duration = movie.Duration;
		Genre = movie.Genre;
		Director = movie.Director;
		Cast = movie.Cast;
		ReleaseDate = movie.ReleaseDate;
		PosterUrl = movie.PosterUrl;
		TrailerUrl = movie.TrailerUrl;
		Rating = movie.Rating;
		Language = movie.Language;
	}

	[Required]
	[MaxLength(200)]
	public string Title { get; init; } = string.Empty;

	[MaxLength(1000)]
	public string Description { get; init; } = string.Empty;

	public int Duration { get; init; }

	[MaxLength(100)]
	public string Genre { get; init; } = string.Empty;

	[MaxLength(200)]
	public string Director { get; init; } = string.Empty;

	[MaxLength(500)]
	public string Cast { get; init; } = string.Empty;

	public DateTime ReleaseDate { get; init; }

	[MaxLength(500)]
	public string PosterUrl { get; init; } = string.Empty;

	[MaxLength(500)]
	public string TrailerUrl { get; init; } = string.Empty;

	[Range(0.0, 10.0)]
	public decimal Rating { get; init; }

	[MaxLength(50)]
	public string Language { get; init; } = string.Empty;
}
