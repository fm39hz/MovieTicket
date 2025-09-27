namespace MovieTicket.Domain.Common.Repository;

using Entity.Payment;

public interface IPaymentRepository : IRepository<PaymentModel> {
	public Task<PaymentModel?> FindByBookingId(Guid bookingId);
	public Task<PaymentModel?> FindByPayOSOrderCode(long orderCode);
	public Task<IEnumerable<PaymentModel>> FindByStatus(string status);
	public Task<PaymentModel> UpdateStatus(Guid paymentId, string status);
}