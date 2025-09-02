namespace MovieTicket.Infrastructure.Persistence.Database;

using Domain.Entity.Movie;
using Domain.Entity.User;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
	[UsedImplicitly] private DbSet<UserModel> Users { get; set; } = null!;
	[UsedImplicitly] private DbSet<MovieModel> Movies { get; set; } = null!;
}
