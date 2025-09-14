namespace MovieTicket.Domain.Entity.Theater;

using System.ComponentModel.DataAnnotations;
using User;

public sealed record BookingModel : BaseModel {
	public BookingModel(BookingModel booking) : base(booking) {
		UserId = booking.UserId;
		ShowtimeId = booking.ShowtimeId;
		SeatNumbers = booking.SeatNumbers;
		BookingDate = booking.BookingDate;
		TotalAmount = booking.TotalAmount;
		PaymentStatus = booking.PaymentStatus;
		BookingStatus = booking.BookingStatus;
		BookingReference = booking.BookingReference;
	}

	[Required]
	public Guid UserId { get; init; }

	[Required]
	public Guid ShowtimeId { get; init; }

	[Required]
	public string SeatNumbers { get; init; } = string.Empty;

	[Required]
	public DateTime BookingDate { get; init; }

	[Range(0.01, 9999.99)]
	public decimal TotalAmount { get; init; }

	[Required]
	public string PaymentStatus { get; init; } = "Pending";

	[Required]
	public string BookingStatus { get; init; } = "Confirmed";

	[Required]
	public string BookingReference { get; init; } = string.Empty;

	public UserModel? User { get; init; }
	public ShowtimeModel? Showtime { get; init; }
}
