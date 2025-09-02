namespace MovieTicket.Infrastructure.DataGenerator.Movie;

using Bogus;
using Domain.Entity.Movie;

public static class MovieDataGenerator {
	private static readonly string[] _genres = [
		"Action", "Adventure", "Comedy", "Drama", "Fantasy", "Horror",
		"Mystery", "Romance", "Thriller", "Science Fiction"
	];

	private static readonly string[] _languages = [
		"English", "Spanish", "French", "German", "Italian", "Japanese",
		"Korean", "Mandarin", "Hindi", "Arabic"
	];

	private static Faker<MovieModel> RuleSet => new Faker<MovieModel>()
		.RuleFor(static movie => movie.Title, static faker => faker.Lorem.Sentence(3, 5).TrimEnd('.'))
		.RuleFor(static movie => movie.Description, static faker => faker.Lorem.Paragraph(3))
		.RuleFor(static movie => movie.Duration, static faker => faker.Random.Int(90, 180))
		.RuleFor(static movie => movie.Genre, static faker => faker.PickRandom(_genres))
		.RuleFor(static movie => movie.Director, static faker => faker.Name.FullName())
		.RuleFor(static movie => movie.Cast, static faker => string.Join(", ", faker.Name.FullName(), faker.Name.FullName(), faker.Name.FullName()))
		.RuleFor(static movie => movie.ReleaseDate, static faker => faker.Date.Past(5))
		.RuleFor(static movie => movie.PosterUrl, static faker => faker.Image.PicsumUrl(400, 600))
		.RuleFor(static movie => movie.TrailerUrl, static faker => faker.Internet.Url())
		.RuleFor(static movie => movie.Rating, static faker => Math.Round(faker.Random.Decimal(1.0m, 10.0m), 1))
		.RuleFor(static movie => movie.Language, static faker => faker.PickRandom(_languages));

	public static MovieModel Generate() => RuleSet.Generate();
}
