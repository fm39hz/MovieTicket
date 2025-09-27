namespace MovieTicket.Application.Service.Contract;

using Domain.Entity.Payment;

public interface IPayOSService {
	public Task<PaymentCreateResult> CreatePaymentLinkAsync(PaymentModel payment);
	public Task<bool> VerifyWebhookDataAsync(string webhookData);
	public Task<PaymentInfo> GetPaymentInfoAsync(long orderCode);
}

public sealed record PaymentCreateResult(string CheckoutUrl, string QrCode);
public sealed record PaymentInfo(string Status, decimal Amount, string Description);