namespace MovieTicket.Domain.Common.Repository;

using Entity.Movie;

public interface IMovieRepository : IRepository<MovieModel> {
	public Task<IEnumerable<MovieModel>> FindByGenre(string genre);
	public Task<IEnumerable<MovieModel>> FindByTitle(string title);
	public Task<IEnumerable<MovieModel>> FindByRating(decimal minRating);
	public Task<IEnumerable<MovieModel>> Search(string? title, string? genre, decimal? minRating);
}
