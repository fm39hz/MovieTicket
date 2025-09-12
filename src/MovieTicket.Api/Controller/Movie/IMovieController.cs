namespace MovieTicket.Api.Controller.Movie;

using Application.Dto.Movie;
using Contract;
using Domain.Entity.Movie;
using Application.Dto.Common;

public interface IMovieController : ICrudController<MovieModel, MovieResponseDto, MovieRequestDto> {
	public Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> Search(MovieSearchRequestDto searchRequest, PaginationRequestDto? pagination);
}
