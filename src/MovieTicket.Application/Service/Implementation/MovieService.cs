namespace MovieTicket.Application.Service.Implementation;

using Contract;
using Domain.Common.Repository;
using Domain.Entity.Movie;

public sealed class MovieService(IMovieRepository repository) : IMovieService {
	public async Task<MovieModel?> FindOne(Guid id) => await repository.FindOne(id);

	public async Task<IEnumerable<MovieModel>> FindAll() => await repository.FindAll();

	public async Task<MovieModel> Create(MovieModel entity) => await repository.Create(entity);

	public async Task<MovieModel> Update(Guid id, MovieModel entity) {
		var movie = new MovieModel(entity) { Id = id };
		return await repository.Update(movie);
	}

	public async Task<int> Delete(Guid id) => await repository.Delete(id);

	public async Task<IEnumerable<MovieModel>> FindByGenre(string genre) => await repository.FindByGenre(genre);

	public async Task<IEnumerable<MovieModel>> FindByTitle(string title) => await repository.FindByTitle(title);

	public async Task<IEnumerable<MovieModel>> FindByRating(decimal minRating) => await repository.FindByRating(minRating);
}