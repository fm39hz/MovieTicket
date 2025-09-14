namespace MovieTicket.Infrastructure.Persistence.Repository;

using Database;
using Domain.Common.Repository;
using Domain.Entity.Theater;
using Microsoft.EntityFrameworkCore;

public sealed class ShowtimeRepository(ApplicationDbContext context) : CrudRepository<ShowtimeModel>(context), IShowtimeRepository {
	public async Task<IEnumerable<ShowtimeModel>> FindByMovieId(Guid movieId) =>
		await Entities
			.Where(s => s.MovieId == movieId && s.Status == "Active")
			.OrderBy(s => s.Date)
			.ThenBy(s => s.StartTime)
			.ToListAsync();

	public async Task<IEnumerable<ShowtimeModel>> FindByTheaterId(Guid theaterId) =>
		await Entities
			.Where(s => s.TheaterId == theaterId && s.Status == "Active")
			.OrderBy(s => s.Date)
			.ThenBy(s => s.StartTime)
			.ToListAsync();

	public async Task<IEnumerable<ShowtimeModel>> FindByScreenId(Guid screenId) =>
		await Entities
			.Where(s => s.ScreenId == screenId && s.Status == "Active")
			.OrderBy(s => s.Date)
			.ThenBy(s => s.StartTime)
			.ToListAsync();

	public async Task<IEnumerable<ShowtimeModel>> FindByDate(DateOnly showDate) =>
		await Entities
			.Where(s => s.Date == showDate && s.Status == "Active")
			.OrderBy(s => s.StartTime)
			.ToListAsync();

	public async Task<IEnumerable<ShowtimeModel>> FindByDateRange(DateOnly fromDate, DateOnly toDate) =>
		await Entities
			.Where(s => s.Date >= fromDate && s.Date <= toDate && s.Status == "Active")
			.OrderBy(s => s.Date)
			.ThenBy(s => s.StartTime)
			.ToListAsync();

	public async Task<IEnumerable<ShowtimeModel>> FindAvailableShowtimes(Guid movieId, DateOnly? showDate = null) {
		var query = Entities
			.Where(s => s.MovieId == movieId && s.Status == "Active" && s.AvailableSeats > 0);

		if (showDate.HasValue) {
			query = query.Where(s => s.Date == showDate.Value);
		}

		return await query
			.OrderBy(s => s.Date)
			.ThenBy(s => s.StartTime)
			.ToListAsync();
	}

	public async Task<IEnumerable<ShowtimeModel>> FindNowPlaying() {
		var today = DateOnly.FromDateTime(DateTime.Today);
		var futureDate = today.AddDays(30);

		return await Entities
			.Where(s => s.Date >= today && s.Date <= futureDate && s.Status == "Active")
			.OrderBy(s => s.Date)
			.ThenBy(s => s.StartTime)
			.ToListAsync();
	}

	public async Task<IEnumerable<ShowtimeModel>> FindByMovieAndTheater(Guid movieId, Guid theaterId) =>
		await Entities
			.Where(s => s.MovieId == movieId && s.TheaterId == theaterId && s.Status == "Active")
			.OrderBy(s => s.Date)
			.ThenBy(s => s.StartTime)
			.ToListAsync();
}
