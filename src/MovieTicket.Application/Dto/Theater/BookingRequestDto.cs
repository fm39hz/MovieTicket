namespace MovieTicket.Application.Dto.Theater;

using Domain.Entity.Theater;

public record BookingRequestDto(
	Guid UserId,
	Guid ShowtimeId,
	string[] SeatNumbers,
	decimal TotalAmount,
	string PaymentStatus,
	string BookingStatus
) : IRequestDto<BookingModel> {
	public BookingModel ToModel() => new() {
		UserId = UserId,
		ShowtimeId = ShowtimeId,
		SeatNumbers = string.Join(",", SeatNumbers),
		BookingDate = DateTime.UtcNow,
		TotalAmount = TotalAmount,
		PaymentStatus = PaymentStatus,
		BookingStatus = BookingStatus,
		BookingReference = $"BK{DateTime.Now.Ticks.ToString()[^6..]}{new Random().Next(1000, 9999)}"
	};
}