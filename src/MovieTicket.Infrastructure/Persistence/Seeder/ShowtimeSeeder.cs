namespace MovieTicket.Infrastructure.Persistence.Seeder;

using DataGenerator.Theater;
using Domain.Entity.Movie;
using Domain.Entity.Theater;
using Microsoft.EntityFrameworkCore;

public class ShowtimeSeeder : ISeeder {
	private const int SHOWTIMES_PER_MOVIE_THEATER = 3;

	public bool SeedData(DbContext context) {
		try {
			var movieCount = context.Set<MovieModel>().Count();
			var theaterCount = context.Set<TheaterModel>().Count();
			var expectedShowtimes = movieCount * theaterCount * SHOWTIMES_PER_MOVIE_THEATER;
			var showtimeCount = context.Set<ShowtimeModel>().Count();

			if (showtimeCount >= expectedShowtimes || movieCount == 0 || theaterCount == 0) {
				return false;
			}

			CreateShowtimes(context);
			context.SaveChanges();
			return true;
		}
		catch (Exception) {
			return false;
		}
	}

	public async Task<bool> SeedDataAsync(DbContext context, CancellationToken cancellationToken = default) {
		try {
			var movieCount = await context.Set<MovieModel>().CountAsync(cancellationToken);
			var theaterCount = await context.Set<TheaterModel>().CountAsync(cancellationToken);
			var expectedShowtimes = movieCount * theaterCount * SHOWTIMES_PER_MOVIE_THEATER;
			var showtimeCount = await context.Set<ShowtimeModel>().CountAsync(cancellationToken);

			if (showtimeCount >= expectedShowtimes || movieCount == 0 || theaterCount == 0) {
				return false;
			}

			CreateShowtimes(context);
			await context.SaveChangesAsync(cancellationToken);
			return true;
		}
		catch (Exception) {
			return false;
		}
	}

	private static void CreateShowtimes(DbContext context) {
		var showtimes = context.Set<ShowtimeModel>();
		var movies = context.Set<MovieModel>().ToList();
		var theaters = context.Set<TheaterModel>().ToList();
		var screens = context.Set<ScreenModel>().ToList();
		var existingShowtimes = context.Set<ShowtimeModel>().ToList();

		foreach (var movie in movies) {
			foreach (var theater in theaters) {
				var theaterScreens = screens.Where(s => s.TheaterId == theater.Id).ToList();
				if (theaterScreens.Count == 0) {
					continue;
				}

				var existingCount = existingShowtimes.Count(s => s.MovieId == movie.Id && s.TheaterId == theater.Id);

				for (var i = existingCount; i < SHOWTIMES_PER_MOVIE_THEATER; i++) {
					var randomScreen = theaterScreens[new Random().Next(theaterScreens.Count)];
					var showtime = ShowtimeDataGenerator.Generate(movie.Id, theater.Id, randomScreen.Id);
					showtimes.Add(showtime);
				}
			}
		}
	}
}
