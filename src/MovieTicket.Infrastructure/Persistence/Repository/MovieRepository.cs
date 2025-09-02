namespace MovieTicket.Infrastructure.Persistence.Repository;

using Database;
using Domain.Common.Repository;
using Domain.Entity.Movie;
using Microsoft.EntityFrameworkCore;

public sealed class MovieRepository(ApplicationDbContext context) : CrudRepository<MovieModel>(context), IMovieRepository {
	public async Task<IEnumerable<MovieModel>> FindByGenre(string genre) =>
		await Entities
			.Where(m => m.Genre.Contains(genre))
			.ToListAsync();

	public async Task<IEnumerable<MovieModel>> FindByTitle(string title) =>
		await Entities
			.Where(m => m.Title.Contains(title))
			.ToListAsync();

	public async Task<IEnumerable<MovieModel>> FindByRating(decimal minRating) =>
		await Entities
			.Where(m => m.Rating >= minRating)
			.ToListAsync();
}