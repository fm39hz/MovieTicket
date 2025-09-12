namespace MovieTicket.Api.Controller.Movie;

using Application.Dto.Movie;
using Contract;
using Domain.Entity.Movie;
using MovieTicket.Application.Dto.Common;

public interface IMovieController : ICrudController<MovieModel, MovieResponseDto, MovieRequestDto> {
	public Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> FindByGenre(string genre);
	public Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> Search(string title, PaginationRequestDto? pagination);
	public Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> FindByRating(decimal minRating);
}
