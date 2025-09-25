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

	public async Task<IEnumerable<ShowtimeModel>> FindByDate(DateTime showDate) =>
		await Entities
			.Where(s => s.Date.Date == showDate.Date && s.Status == "Active")
			.OrderBy(s => s.StartTime)
			.ToListAsync();

	public async Task<IEnumerable<ShowtimeModel>> FindByDateRange(DateTime fromDate, DateTime toDate) =>
		await Entities
			.Where(s => s.Date.Date >= fromDate.Date && s.Date.Date <= toDate.Date && s.Status == "Active")
			.OrderBy(s => s.Date)
			.ThenBy(s => s.StartTime)
			.ToListAsync();

	public async Task<IEnumerable<ShowtimeModel>> FindAvailableShowtimes(Guid movieId, DateTime? showDate = null) {
		var query = Entities
			.Where(s => s.MovieId == movieId && s.Status == "Active" && s.AvailableSeats > 0);

		if (showDate.HasValue) {
			query = query.Where(s => s.Date.Date == showDate.Value.Date);
		}

		return await query
			.OrderBy(s => s.Date)
			.ThenBy(s => s.StartTime)
			.ToListAsync();
	}

	public async Task<IEnumerable<ShowtimeModel>> FindNowPlaying() {
		var today = DateTime.Today;
		var futureDate = today.AddDays(30);

		return await Entities
			.Where(s => s.Date.Date >= today && s.Date.Date <= futureDate && s.Status == "Active")
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
