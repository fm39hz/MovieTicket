namespace MovieTicket.Application.Service.Implementation;

using Contract;
using Domain.Common.Repository;
using Domain.Entity.Theater;

public sealed class BookingService(IBookingRepository repository) : IBookingService {
	public async Task<BookingModel?> FindOne(Guid id) => await repository.FindOne(id);

	public async Task<IEnumerable<BookingModel>> FindAll() => await repository.FindAll();

	public async Task<BookingModel> Create(BookingModel entity) => await repository.Create(entity);

	public async Task<BookingModel> Update(Guid id, BookingModel entity) {
		var booking = new BookingModel(entity) { Id = id };
		return await repository.Update(booking);
	}

	public async Task<int> Delete(Guid id) => await repository.Delete(id);

	public async Task<IEnumerable<BookingModel>> FindByUserId(Guid userId) => await repository.FindByUserId(userId);

	public async Task<IEnumerable<BookingModel>> FindByShowtimeId(Guid showtimeId) => await repository.FindByShowtimeId(showtimeId);

	public async Task<BookingModel?> FindByBookingReference(string bookingReference) => await repository.FindByBookingReference(bookingReference);

	public async Task<IEnumerable<BookingModel>> FindByDateRange(DateTime fromDate, DateTime toDate) => await repository.FindByDateRange(fromDate, toDate);

	public async Task<IEnumerable<BookingModel>> FindByPaymentStatus(string paymentStatus) => await repository.FindByPaymentStatus(paymentStatus);

	public async Task<IEnumerable<BookingModel>> FindByBookingStatus(string bookingStatus) => await repository.FindByBookingStatus(bookingStatus);

	public async Task<IEnumerable<BookingModel>> GetPopularMovies(int topCount = 10) => await repository.GetPopularMovies(topCount);

	public string GenerateBookingReference() {
		var timestamp = DateTime.Now.Ticks.ToString();
		var random = new Random().Next(1000, 9999);
		return $"BK{timestamp[^6..]}{random}";
	}

	public async Task<BookingModel> BookTickets(Guid userId, Guid showtimeId, string[] seatNumbers) {
		var bookingReference = GenerateBookingReference();

		var booking = new BookingModel() {
			Id = Guid.NewGuid(),
			UserId = userId,
			ShowtimeId = showtimeId,
			SeatNumbers = string.Join(",", seatNumbers),
			BookingDate = DateTime.UtcNow,
			BookingStatus = "Confirmed",
			PaymentStatus = "Pending",
			BookingReference = bookingReference,
			TotalAmount = seatNumbers.Length * 12.50m
		};

		return await Create(booking);
	}
}
