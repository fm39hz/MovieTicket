namespace MovieTicket.Domain.Common.Repository;

using Entity.Theater;

public interface IBookingRepository : IRepository<BookingModel> {
	public Task<IEnumerable<BookingModel>> FindByUserId(Guid userId);
	public Task<IEnumerable<BookingModel>> FindByUserIdWithDetails(Guid userId);
	public Task<IEnumerable<BookingModel>> FindByUserIdWithDetailsAndFilters(Guid userId, string? status = null, DateTime? dateFrom = null, DateTime? dateTo = null, int pageSize = 20, int pageNumber = 1);
	public Task<IEnumerable<BookingModel>> FindByShowtimeId(Guid showtimeId);
	public Task<BookingModel?> FindByBookingReference(string bookingReference);
	public Task<IEnumerable<BookingModel>> FindByDateRange(DateTime fromDate, DateTime toDate);
	public Task<IEnumerable<BookingModel>> FindByPaymentStatus(string paymentStatus);
	public Task<IEnumerable<BookingModel>> FindByBookingStatus(string bookingStatus);
	public Task<IEnumerable<BookingModel>> GetPopularMovies(int topCount = 10);
}