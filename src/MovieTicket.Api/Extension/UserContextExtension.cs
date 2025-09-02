namespace MovieTicket.Api.Extension;

using Infrastructure.Configuration;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Seeder;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

public static class DatabaseContextExtension {
	[UsedImplicitly]
	public static IServiceCollection
		AddDatabaseContext(this IServiceCollection service, WebApplicationBuilder builder) {
		var mariaDbConfiguration = new MariaDbConfiguration(builder.Configuration);
		var userSeed = new UserSeedConfiguration(builder.Configuration);

		builder.Services.AddMySQLServer<ApplicationDbContext>(mariaDbConfiguration.ConnectionString);

		service.AddSingleton(userSeed);
		service.AddTransient<AdminSeeder>();
		service.AddTransient<UserSeeder>();
		service.AddTransient<MovieSeeder>();
		service.AddTransient<DatabaseSeeder>();

		service.AddDbContext<ApplicationDbContext>(optionsBuilder => {
			optionsBuilder.UseMySQL().UseSnakeCaseNamingConvention();
			var seeders = new List<ISeeder> { new AdminSeeder(userSeed), new UserSeeder(), new MovieSeeder() };
			var databaseSeeder = new DatabaseSeeder(seeders);
			optionsBuilder.UseSeeding((context, _) => databaseSeeder.SeedAll(context))
				.UseAsyncSeeding(async (context, _, cancellationToken) =>
					await databaseSeeder.SeedAllAsync(context, cancellationToken));
		});

		return service;
	}
}
