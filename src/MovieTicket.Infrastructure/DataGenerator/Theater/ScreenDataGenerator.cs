namespace MovieTicket.Infrastructure.DataGenerator.Theater;

using Bogus;
using Domain.Entity.Theater;

public static class ScreenDataGenerator {
	private static Faker<ScreenModel> RuleSet => new Faker<ScreenModel>()
		.RuleFor(static screen => screen.ScreenNumber, static faker => faker.Random.Int(1, 16))
		.RuleFor(static screen => screen.ScreenType, static faker => faker.Commerce.ProductName())
		.RuleFor(static screen => screen.TotalSeats, static faker => faker.Random.Int(80, 300))
		.RuleFor(static screen => screen.AudioSystem, static faker => faker.Commerce.Product())
		.RuleFor(static screen => screen.SeatLayout, static faker => faker.Lorem.Paragraph(2));

	public static ScreenModel Generate(Guid theaterId) => RuleSet
		.RuleFor(static screen => screen.TheaterId, theaterId)
		.Generate();
}
