namespace MovieTicket.Application.Dto.Movie;

using Domain.Entity.Movie;

public record MovieRequestDto(
	string Title,
	string Description,
	int Duration,
	string Genre,
	string Director,
	string Cast,
	DateTime ReleaseDate,
	string PosterUrl,
	string TrailerUrl,
	decimal Rating,
	string Language
) : IRequestDto<MovieModel> {
	public MovieModel ToModel() => new() {
		Title = Title,
		Description = Description,
		Duration = Duration,
		Genre = Genre,
		Director = Director,
		Cast = Cast,
		ReleaseDate = ReleaseDate,
		PosterUrl = PosterUrl,
		TrailerUrl = TrailerUrl,
		Rating = Rating,
		Language = Language
	};
}