namespace MovieTicket.Application.Service.Implementation;

using Contract;
using Dto.Payment;
using Domain.Common.Repository;
using Domain.Entity.Payment;

public sealed class PaymentService(IPaymentRepository paymentRepository, IPayOSService payOSService) : IPaymentService {
	public async Task<PaymentResponseDto> CreatePaymentLinkAsync(CreatePaymentRequestDto request) {
		var payment = request.ToModel();
		var createdPayment = await paymentRepository.Create(payment);

		try {
			var paymentResult = await payOSService.CreatePaymentLinkAsync(createdPayment);

			var updatedPayment = new PaymentModel(createdPayment) {
				PaymentUrl = paymentResult.CheckoutUrl,
				UpdatedAt = DateTime.UtcNow
			};

			var finalPayment = await paymentRepository.Update(updatedPayment);
			return new PaymentResponseDto(finalPayment);
		}
		catch (Exception) {
			await paymentRepository.UpdateStatus(createdPayment.Id, "FAILED");
			throw;
		}
	}

	public async Task<PaymentResponseDto?> GetPaymentByBookingIdAsync(Guid bookingId) {
		var payment = await paymentRepository.FindByBookingId(bookingId);
		return payment != null ? new PaymentResponseDto(payment) : null;
	}

	public async Task<PaymentResponseDto?> GetPaymentByIdAsync(Guid paymentId) {
		var payment = await paymentRepository.FindOne(paymentId);
		return payment != null ? new PaymentResponseDto(payment) : null;
	}

	public async Task<bool> VerifyWebhookAsync(PaymentWebhookDto webhook) {
		try {
			// In a real implementation, you would use the PayOS webhook verification
			// var webhookData = payOSService.VerifyPaymentWebhookData(rawWebhookData);

			if (webhook.Data?.OrderCode == null) return false;

			var payment = await paymentRepository.FindByPayOSOrderCode(webhook.Data.OrderCode);
			if (payment == null) return false;

			if (webhook.Success && webhook.Data.Amount == payment.Amount) {
				await paymentRepository.UpdateStatus(payment.Id, "PAID");
				return true;
			}

			if (!webhook.Success) {
				await paymentRepository.UpdateStatus(payment.Id, "CANCELLED");
			}

			return false;
		}
		catch (Exception) {
			return false;
		}
	}

	public async Task<PaymentResponseDto> UpdatePaymentStatusAsync(Guid paymentId, string status) {
		var updatedPayment = await paymentRepository.UpdateStatus(paymentId, status);
		return new PaymentResponseDto(updatedPayment);
	}

	public async Task<IEnumerable<PaymentResponseDto>> GetPaymentsByStatusAsync(string status) {
		var payments = await paymentRepository.FindByStatus(status);
		return payments.Select(payment => new PaymentResponseDto(payment));
	}
}