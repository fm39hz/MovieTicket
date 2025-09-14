namespace MovieTicket.Infrastructure.Persistence.Seeder;

using DataGenerator.Theater;
using Domain.Entity.Theater;
using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;

public class BookingSeeder : ISeeder {
	/// <summary>
	/// 30% of showtimes should have bookings
	/// </summary>
	private const int TARGET_BOOKING_PERCENTAGE = 30;

	public bool SeedData(DbContext context) {
		try {
			var showtimeCount = context.Set<ShowtimeModel>().Count();
			var userCount = context.Set<UserModel>().Count();
			var bookingCount = context.Set<BookingModel>().Count();
			var targetBookings = showtimeCount * TARGET_BOOKING_PERCENTAGE / 100;

			if (bookingCount >= targetBookings || userCount == 0 || showtimeCount == 0) {
				return false;
			}

			CreateBookings(context, targetBookings - bookingCount);
			context.SaveChanges();
			return true;
		}
		catch (Exception) {
			return false;
		}
	}

	public async Task<bool> SeedDataAsync(DbContext context, CancellationToken cancellationToken = default) {
		try {
			var showtimeCount = await context.Set<ShowtimeModel>().CountAsync(cancellationToken);
			var userCount = await context.Set<UserModel>().CountAsync(cancellationToken);
			var bookingCount = await context.Set<BookingModel>().CountAsync(cancellationToken);
			var targetBookings = showtimeCount * TARGET_BOOKING_PERCENTAGE / 100;

			if (bookingCount >= targetBookings || userCount == 0 || showtimeCount == 0) {
				return false;
			}

			CreateBookings(context, targetBookings - bookingCount);
			await context.SaveChangesAsync(cancellationToken);
			return true;
		}
		catch (Exception) {
			return false;
		}
	}

	private static void CreateBookings(DbContext context, int bookingsToCreate) {
		var bookings = context.Set<BookingModel>();
		var users = context.Set<UserModel>().ToList();
		var showtimes = context.Set<ShowtimeModel>().ToList();
		var existingBookings = context.Set<BookingModel>().Select(b => b.ShowtimeId).ToHashSet();
		var random = new Random();

		var availableShowtimes = showtimes.Where(s => !existingBookings.Contains(s.Id)).ToList();

		for (var i = 0; i < bookingsToCreate && i < availableShowtimes.Count; i++) {
			var randomUser = users[random.Next(users.Count)];
			var randomShowtime = availableShowtimes[i];

			var booking = BookingDataGenerator.Generate(randomUser.Id, randomShowtime.Id);
			bookings.Add(booking);
		}
	}
}
