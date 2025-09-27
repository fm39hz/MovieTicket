namespace MovieTicket.Application.Dto.Payment;

using System.ComponentModel.DataAnnotations;
using Domain.Entity.Payment;

public sealed record CreatePaymentRequestDto : IRequestDto<PaymentModel> {
	[Required]
	public Guid BookingId { get; init; }

	[Required]
	[Range(0.01, 999999.99)]
	public decimal Amount { get; init; }

	[Required]
	[MaxLength(500)]
	public string Description { get; init; } = string.Empty;

	[Required]
	[MaxLength(1000)]
	public string ReturnUrl { get; init; } = string.Empty;

	[Required]
	[MaxLength(1000)]
	public string CancelUrl { get; init; } = string.Empty;

	public PaymentModel ToModel() {
		var orderCode = DateTimeOffset.Now.ToUnixTimeSeconds();

		return new PaymentModel() {
			Id = Guid.NewGuid(),
			BookingId = BookingId,
			PayOSOrderCode = orderCode,
			Amount = Amount,
			Description = Description,
			Status = "PENDING",
			ReturnUrl = ReturnUrl,
			CancelUrl = CancelUrl,
			CreatedAt = DateTime.UtcNow
		};
	}
}