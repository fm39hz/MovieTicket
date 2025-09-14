namespace MovieTicket.Infrastructure.Persistence.Seeder;

using DataGenerator.Theater;
using Domain.Entity.Theater;
using Microsoft.EntityFrameworkCore;

public class TheaterSeeder : ISeeder {
	private const int TARGET_SEED_NUMBER = 12;

	public bool SeedData(DbContext context) {
		try {
			var theaterCount = context.Set<TheaterModel>().Count();
			if (theaterCount >= TARGET_SEED_NUMBER) {
				return false;
			}

			CreateTheaters(context, theaterCount);
			context.SaveChanges();
			return true;
		}
		catch (Exception) {
			return false;
		}
	}

	public async Task<bool> SeedDataAsync(DbContext context, CancellationToken cancellationToken = default) {
		try {
			var theaterCount = await context.Set<TheaterModel>().CountAsync(cancellationToken);
			if (theaterCount >= TARGET_SEED_NUMBER) {
				return false;
			}

			CreateTheaters(context, theaterCount);
			await context.SaveChangesAsync(cancellationToken);
			return true;
		}
		catch (Exception) {
			return false;
		}
	}

	private static void CreateTheaters(DbContext context, int existingTheaterCount) {
		var theaters = context.Set<TheaterModel>();

		for (var i = 0; i < TARGET_SEED_NUMBER - existingTheaterCount; i++) {
			var theater = TheaterDataGenerator.Generate();
			theaters.Add(theater);
		}
	}
}