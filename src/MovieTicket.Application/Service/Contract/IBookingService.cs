namespace MovieTicket.Application.Service.Contract;

using Domain.Entity.Theater;

public interface IBookingService : ICrudService<BookingModel> {
	public Task<IEnumerable<BookingModel>> FindByUserId(Guid userId);
	public Task<IEnumerable<BookingModel>> FindByShowtimeId(Guid showtimeId);
	public Task<BookingModel?> FindByBookingReference(string bookingReference);
	public Task<IEnumerable<BookingModel>> FindByDateRange(DateTime fromDate, DateTime toDate);
	public Task<IEnumerable<BookingModel>> FindByPaymentStatus(string paymentStatus);
	public Task<IEnumerable<BookingModel>> FindByBookingStatus(string bookingStatus);
	public Task<IEnumerable<BookingModel>> GetPopularMovies(int topCount = 10);
	public string GenerateBookingReference();
	public Task<BookingModel> BookTickets(Guid userId, Guid showtimeId, string[] seatNumbers);
}
