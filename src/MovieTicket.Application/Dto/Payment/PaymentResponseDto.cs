namespace MovieTicket.Application.Dto.Payment;

using Common;
using Domain.Entity.Payment;

public sealed record PaymentResponseDto : IResponseDto {
	public PaymentResponseDto(PaymentModel payment) {
		Id = payment.Id;
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

	public Guid Id { get; init; }
	public Guid BookingId { get; init; }
	public long PayOSOrderCode { get; init; }
	public decimal Amount { get; init; }
	public string Description { get; init; } = string.Empty;
	public string Status { get; init; } = string.Empty;
	public string PaymentUrl { get; init; } = string.Empty;
	public string ReturnUrl { get; init; } = string.Empty;
	public string CancelUrl { get; init; } = string.Empty;
	public DateTime CreatedAt { get; init; }
	public DateTime? UpdatedAt { get; init; }
}