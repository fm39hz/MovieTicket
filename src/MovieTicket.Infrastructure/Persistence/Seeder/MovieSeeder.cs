namespace MovieTicket.Infrastructure.Persistence.Seeder;

using DataGenerator.Movie;
using Domain.Entity.Movie;
using Microsoft.EntityFrameworkCore;

public class MovieSeeder : ISeeder {
	private const int TARGET_SEED_NUMBER = 25;

	public bool SeedData(DbContext context) {
		try {
			var movieCount = context.Set<MovieModel>().Count();
			if (movieCount >= TARGET_SEED_NUMBER) {
				return false;
			}

			CreateMovies(context, movieCount);
			context.SaveChanges();
			return true;
		}
		catch (Exception) {
			// Table doesn't exist yet, skip seeding
			return false;
		}
	}

	public async Task<bool> SeedDataAsync(DbContext context, CancellationToken cancellationToken = default) {
		try {
			var movieCount = await context.Set<MovieModel>().CountAsync(cancellationToken);
			if (movieCount >= TARGET_SEED_NUMBER) {
				return false;
			}

			CreateMovies(context, movieCount);
			await context.SaveChangesAsync(cancellationToken);
			return true;
		}
		catch (Exception) {
			// Table doesn't exist yet, skip seeding
			return false;
		}
	}

	private static void CreateMovies(DbContext context, int existingMovieCount) {
		var movies = context.Set<MovieModel>();

		for (var i = 0; i < TARGET_SEED_NUMBER - existingMovieCount; i++) {
			var movie = MovieDataGenerator.Generate();
			movies.Add(movie);
		}
	}
}