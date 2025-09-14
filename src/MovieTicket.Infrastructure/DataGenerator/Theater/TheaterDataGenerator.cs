namespace MovieTicket.Infrastructure.DataGenerator.Theater;

using Bogus;
using Domain.Entity.Theater;

public static class TheaterDataGenerator {
	private static Faker<TheaterModel> RuleSet => new Faker<TheaterModel>()
		.RuleFor(static theater => theater.Name, static faker => $"{faker.Company.CompanyName()} Cinema")
		.RuleFor(static theater => theater.Address, static faker => faker.Address.StreetAddress())
		.RuleFor(static theater => theater.City, static faker => faker.Address.City())
		.RuleFor(static theater => theater.State, static faker => faker.Address.StateAbbr())
		.RuleFor(static theater => theater.ZipCode, static faker => faker.Address.ZipCode())
		.RuleFor(static theater => theater.PhoneNumber, static faker => faker.Phone.PhoneNumber())
		.RuleFor(static theater => theater.ScreenCount, static faker => faker.Random.Int(4, 16))
		.RuleFor(static theater => theater.ParkingAvailable, static faker => faker.Random.Bool(0.8f))
		.RuleFor(static theater => theater.Facilities, static faker => string.Join(", ", faker.Lorem.Words(faker.Random.Int(3, 7))));

	public static TheaterModel Generate() => RuleSet.Generate();
}
