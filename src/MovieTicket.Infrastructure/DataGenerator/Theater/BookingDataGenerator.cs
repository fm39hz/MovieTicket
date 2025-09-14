namespace MovieTicket.Infrastructure.DataGenerator.Theater;

using Bogus;
using Domain.Entity.Theater;

public static class BookingDataGenerator {
	private static Faker<BookingModel> RuleSet => new Faker<BookingModel>()
		.RuleFor(booking => booking.SeatNumbers, faker => string.Join(",", faker.Make(faker.Random.Int(1, 4), () => faker.Random.AlphaNumeric(3))))
		.RuleFor(booking => booking.BookingDate, faker => faker.Date.Past(1))
		.RuleFor(booking => booking.TotalAmount, faker => Math.Round(faker.Random.Decimal(12.50m, 75.00m), 2))
		.RuleFor(booking => booking.PaymentStatus, faker => faker.PickRandom("Paid", "Pending", "Failed"))
		.RuleFor(booking => booking.BookingStatus, faker => faker.PickRandom("Confirmed", "Cancelled", "Completed"))
		.RuleFor(booking => booking.BookingReference, faker => faker.Random.AlphaNumeric(8).ToUpper(System.Globalization.CultureInfo.CurrentCulture));

	public static BookingModel Generate(Guid userId, Guid showtimeId) => RuleSet
		.RuleFor(static booking => booking.UserId, userId)
		.RuleFor(static booking => booking.ShowtimeId, showtimeId)
		.Generate();
}
