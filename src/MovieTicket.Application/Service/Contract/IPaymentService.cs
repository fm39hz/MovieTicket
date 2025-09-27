namespace MovieTicket.Application.Service.Contract;

using Dto.Payment;
using Domain.Entity.Payment;

public interface IPaymentService {
	public Task<PaymentResponseDto> CreatePaymentLinkAsync(CreatePaymentRequestDto request);
	public Task<PaymentResponseDto?> GetPaymentByBookingIdAsync(Guid bookingId);
	public Task<PaymentResponseDto?> GetPaymentByIdAsync(Guid paymentId);
	public Task<bool> VerifyWebhookAsync(PaymentWebhookDto webhook);
	public Task<PaymentResponseDto> UpdatePaymentStatusAsync(Guid paymentId, string status);
	public Task<IEnumerable<PaymentResponseDto>> GetPaymentsByStatusAsync(string status);
}