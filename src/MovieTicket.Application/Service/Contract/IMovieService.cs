namespace MovieTicket.Application.Service.Contract;

using Domain.Entity.Movie;
using Dto.Movie;

public interface IMovieService : ICrudService<MovieModel> {
	public Task<IEnumerable<MovieModel>> FindByGenre(string genre);
	public Task<IEnumerable<MovieModel>> FindByTitle(string title);
	public Task<IEnumerable<MovieModel>> FindByRating(decimal minRating);
	public Task<IEnumerable<MovieModel>> Search(MovieSearchRequestDto searchRequest);
}
