namespace MovieTicket.Infrastructure.Persistence.Database;

using Domain.Entity.Movie;
using Domain.Entity.Payment;
using Domain.Entity.Theater;
using Domain.Entity.User;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
	[UsedImplicitly] private DbSet<UserModel> Users { get; set; } = null!;
	[UsedImplicitly] private DbSet<MovieModel> Movies { get; set; } = null!;
	[UsedImplicitly] private DbSet<TheaterModel> Theaters { get; set; } = null!;
	[UsedImplicitly] private DbSet<ScreenModel> Screens { get; set; } = null!;
	[UsedImplicitly] private DbSet<ShowtimeModel> Showtimes { get; set; } = null!;
	[UsedImplicitly] private DbSet<BookingModel> Bookings { get; set; } = null!;
	[UsedImplicitly] private DbSet<PaymentModel> Payments { get; set; } = null!;
}
