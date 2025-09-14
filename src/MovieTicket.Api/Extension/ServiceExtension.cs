namespace MovieTicket.Api.Extension;

using Application.Service.Contract;
using Application.Service.Implementation;
using Domain.Common.Repository;
using Infrastructure.Persistence.Repository;
using JetBrains.Annotations;

public static class ServiceExtension {
	[UsedImplicitly]
	public static IServiceCollection AddServices(this IServiceCollection service) {
		service.AddScoped<IUserRepository, UserRepository>();
		service.AddScoped<IUserService, UserService>();
		service.AddScoped<IMovieRepository, MovieRepository>();
		service.AddScoped<IMovieService, MovieService>();
		service.AddScoped<ITheaterRepository, TheaterRepository>();
		service.AddScoped<ITheaterService, TheaterService>();
		service.AddScoped<IScreenRepository, ScreenRepository>();
		service.AddScoped<IScreenService, ScreenService>();
		service.AddScoped<IShowtimeRepository, ShowtimeRepository>();
		service.AddScoped<IShowtimeService, ShowtimeService>();
		service.AddScoped<IBookingRepository, BookingRepository>();
		service.AddScoped<IBookingService, BookingService>();
		service.AddScoped<IStatisticsService, StatisticsService>();
		return service;
	}
}
