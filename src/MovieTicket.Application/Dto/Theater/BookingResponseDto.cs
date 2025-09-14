namespace MovieTicket.Application.Dto.Theater;

using Common;
using Domain.Entity.Theater;

public class BookingResponseDto(BookingModel booking) : IResponseDto {
	public Guid Id { get; init; } = booking.Id;
	public Guid UserId { get; init; } = booking.UserId;
	public Guid ShowtimeId { get; init; } = booking.ShowtimeId;
	public string[] SeatNumbers { get; init; } = booking.SeatNumbers.Split(',', StringSplitOptions.RemoveEmptyEntries);
	public DateTime BookingDate { get; init; } = booking.BookingDate;
	public decimal TotalAmount { get; init; } = booking.TotalAmount;
	public string PaymentStatus { get; init; } = booking.PaymentStatus;
	public string BookingStatus { get; init; } = booking.BookingStatus;
	public string BookingReference { get; init; } = booking.BookingReference;
}