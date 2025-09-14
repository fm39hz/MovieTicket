namespace MovieTicket.Infrastructure.Persistence.Seeder;

using DataGenerator.Theater;
using Domain.Entity.Theater;
using Microsoft.EntityFrameworkCore;

public class ScreenSeeder : ISeeder {
	private const int SCREENS_PER_THEATER = 4;

	public bool SeedData(DbContext context) {
		try {
			var screenCount = context.Set<ScreenModel>().Count();
			var theaterCount = context.Set<TheaterModel>().Count();
			var expectedScreens = theaterCount * SCREENS_PER_THEATER;

			if (screenCount >= expectedScreens) {
				return false;
			}

			CreateScreens(context);
			context.SaveChanges();
			return true;
		}
		catch (Exception) {
			return false;
		}
	}

	public async Task<bool> SeedDataAsync(DbContext context, CancellationToken cancellationToken = default) {
		try {
			var screenCount = await context.Set<ScreenModel>().CountAsync(cancellationToken);
			var theaterCount = await context.Set<TheaterModel>().CountAsync(cancellationToken);
			var expectedScreens = theaterCount * SCREENS_PER_THEATER;

			if (screenCount >= expectedScreens) {
				return false;
			}

			CreateScreens(context);
			await context.SaveChangesAsync(cancellationToken);
			return true;
		}
		catch (Exception) {
			return false;
		}
	}

	private static void CreateScreens(DbContext context) {
		var screens = context.Set<ScreenModel>();
		var theaters = context.Set<TheaterModel>().ToList();
		var existingScreens = context.Set<ScreenModel>().ToList();

		foreach (var theater in theaters) {
			var theaterScreens = existingScreens.Count(s => s.TheaterId == theater.Id);

			for (var screenNum = theaterScreens + 1; screenNum <= SCREENS_PER_THEATER; screenNum++) {
				var screen = ScreenDataGenerator.Generate(theater.Id);
				screen = screen with { ScreenNumber = screenNum };
				screens.Add(screen);
			}
		}
	}
}
