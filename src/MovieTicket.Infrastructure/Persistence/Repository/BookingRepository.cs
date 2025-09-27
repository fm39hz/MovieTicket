namespace MovieTicket.Infrastructure.Persistence.Repository;

using Database;
using Domain.Common.Repository;
using Domain.Entity.Theater;
using Microsoft.EntityFrameworkCore;

public sealed class BookingRepository(ApplicationDbContext context) : CrudRepository<BookingModel>(context), IBookingRepository {
	public async Task<IEnumerable<BookingModel>> FindByUserId(Guid userId) =>
		await Entities
			.Where(b => b.UserId == userId)
			.OrderByDescending(b => b.BookingDate)
			.ToListAsync();

	public async Task<IEnumerable<BookingModel>> FindByUserIdWithDetails(Guid userId) =>
		await Entities
			.Include(b => b.Showtime)
				.ThenInclude(s => s!.Movie)
			.Include(b => b.Showtime)
				.ThenInclude(s => s!.Theater)
			.Include(b => b.Showtime)
				.ThenInclude(s => s!.Screen)
			.Where(b => b.UserId == userId)
			.OrderByDescending(b => b.BookingDate)
			.ToListAsync();

	public async Task<IEnumerable<BookingModel>> FindByUserIdWithDetailsAndFilters(
		Guid userId,
		string? status = null,
		DateTime? dateFrom = null,
		DateTime? dateTo = null,
		int pageSize = 20,
		int pageNumber = 1) {

		var query = Entities
			.Include(b => b.Showtime)
				.ThenInclude(s => s!.Movie)
			.Include(b => b.Showtime)
				.ThenInclude(s => s!.Theater)
			.Include(b => b.Showtime)
				.ThenInclude(s => s!.Screen)
			.Where(b => b.UserId == userId);

		// Apply status filter (booking status or payment status)
		if (!string.IsNullOrEmpty(status)) {
			query = query.Where(b => string.Equals(b.BookingStatus, status, StringComparison.OrdinalIgnoreCase) ||
									 string.Equals(b.PaymentStatus, status, StringComparison.OrdinalIgnoreCase));
		}

		// Apply date range filter
		if (dateFrom.HasValue) {
			query = query.Where(b => b.BookingDate >= dateFrom.Value);
		}

		if (dateTo.HasValue) {
			query = query.Where(b => b.BookingDate <= dateTo.Value);
		}

		// Apply pagination
		var skip = (pageNumber - 1) * pageSize;

		return await query
			.OrderByDescending(b => b.BookingDate)
			.Skip(skip)
			.Take(pageSize)
			.ToListAsync();
	}

	public async Task<IEnumerable<BookingModel>> FindByShowtimeId(Guid showtimeId) =>
		await Entities
			.Where(b => b.ShowtimeId == showtimeId)
			.OrderBy(b => b.BookingDate)
			.ToListAsync();

	public async Task<BookingModel?> FindByBookingReference(string bookingReference) =>
		await Entities
			.FirstOrDefaultAsync(b => b.BookingReference == bookingReference);

	public async Task<IEnumerable<BookingModel>> FindByDateRange(DateTime fromDate, DateTime toDate) =>
		await Entities
			.Where(b => b.BookingDate >= fromDate && b.BookingDate <= toDate)
			.OrderBy(b => b.BookingDate)
			.ToListAsync();

	public async Task<IEnumerable<BookingModel>> FindByPaymentStatus(string paymentStatus) =>
		await Entities
			.Where(b => string.Equals(b.PaymentStatus, paymentStatus, StringComparison.OrdinalIgnoreCase))
			.OrderByDescending(b => b.BookingDate)
			.ToListAsync();

	public async Task<IEnumerable<BookingModel>> FindByBookingStatus(string bookingStatus) =>
		await Entities
			.Where(b => string.Equals(b.BookingStatus, bookingStatus, StringComparison.OrdinalIgnoreCase))
			.OrderByDescending(b => b.BookingDate)
			.ToListAsync();

	public async Task<IEnumerable<BookingModel>> GetPopularMovies(int topCount = 10) =>
		await Entities
			.Where(static b => b.BookingStatus == "Confirmed" && b.PaymentStatus == "Paid")
			.GroupBy(static b => b.Showtime!.MovieId)
			.OrderByDescending(static g => g.Count())
			.Take(topCount)
			.SelectMany(static g => g)
			.ToListAsync();
}
