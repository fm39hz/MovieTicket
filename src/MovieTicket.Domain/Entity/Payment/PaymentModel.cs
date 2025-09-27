namespace MovieTicket.Domain.Entity.Payment;

using System.ComponentModel.DataAnnotations;
using Theater;

public sealed record PaymentModel : BaseModel {
	public PaymentModel(PaymentModel payment) : base(payment) {
		BookingId = payment.BookingId;
		PayOSOrderCode = payment.PayOSOrderCode;
		Amount = payment.Amount;
		Description = payment.Description;
		Status = payment.Status;
		PaymentUrl = payment.PaymentUrl;
		ReturnUrl = payment.ReturnUrl;
		CancelUrl = payment.CancelUrl;
		CreatedAt = payment.CreatedAt;
		UpdatedAt = payment.UpdatedAt;
	}

	[Required]
	public Guid BookingId { get; init; }

	[Required]
	public long PayOSOrderCode { get; init; }

	[Range(0.01, 999999.99)]
	public decimal Amount { get; init; }

	[Required]
	[MaxLength(500)]
	public string Description { get; init; } = string.Empty;

	[Required]
	[MaxLength(50)]
	public string Status { get; init; } = "PENDING";

	[MaxLength(1000)]
	public string PaymentUrl { get; init; } = string.Empty;

	[MaxLength(1000)]
	public string ReturnUrl { get; init; } = string.Empty;

	[MaxLength(1000)]
	public string CancelUrl { get; init; } = string.Empty;

	[Required]
	public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

	public DateTime? UpdatedAt { get; init; }

	public BookingModel? Booking { get; init; }
}