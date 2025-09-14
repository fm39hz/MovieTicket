namespace MovieTicket.Infrastructure.DataGenerator.Theater;

using Bogus;
using Domain.Entity.Theater;

public static class ShowtimeDataGenerator {
	private static Faker<ShowtimeModel> RuleSet => new Faker<ShowtimeModel>()
		.RuleFor(showtime => showtime.StartTime, faker => TimeOnly.FromDateTime(faker.Date.Between(
			DateTime.Today.AddHours(9), DateTime.Today.AddHours(22))))
		.RuleFor(showtime => showtime.EndTime, (faker, showtime) => showtime.StartTime.AddMinutes(faker.Random.Int(90, 180)))
		.RuleFor(showtime => showtime.Date, faker => DateOnly.FromDateTime(faker.Date.Between(
			DateTime.Today.AddDays(-30), DateTime.Today.AddDays(60))))
		.RuleFor(showtime => showtime.TicketPrice, faker => Math.Round(faker.Random.Decimal(8.50m, 18.50m), 2))
		.RuleFor(showtime => showtime.AvailableSeats, faker => faker.Random.Int(50, 250))
		.RuleFor(showtime => showtime.BookedSeats, faker => string.Join(",", faker.Lorem.Words(faker.Random.Int(5, 20))))
		.RuleFor(showtime => showtime.Status, faker => faker.PickRandom("Active", "Cancelled", "SoldOut"));

	public static ShowtimeModel Generate(Guid movieId, Guid theaterId, Guid screenId) => RuleSet
		.RuleFor(static showtime => showtime.MovieId, movieId)
		.RuleFor(static showtime => showtime.TheaterId, theaterId)
		.RuleFor(static showtime => showtime.ScreenId, screenId)
		.Generate();
}
