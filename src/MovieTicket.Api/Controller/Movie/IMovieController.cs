namespace MovieTicket.Api.Controller.Movie;

using Application.Dto.Movie;
using Contract;
using Domain.Entity.Movie;

public interface IMovieController : ICrudController<MovieModel, MovieResponseDto, MovieRequestDto> {
	public Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> FindByGenre(string genre);
	public Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> Search(string title);
	public Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> FindByRating(decimal minRating);
}