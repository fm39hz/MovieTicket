namespace MovieTicket.Infrastructure.Persistence.Repository;

using Domain.Common.Repository;
using Domain.Entity.Payment;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

public sealed class PaymentRepository(ApplicationDbContext context) : CrudRepository<PaymentModel>(context), IPaymentRepository {
	public async Task<PaymentModel?> FindByBookingId(Guid bookingId) =>
		await Entities.FirstOrDefaultAsync(p => p.BookingId == bookingId);

	public async Task<PaymentModel?> FindByPayOSOrderCode(long orderCode) =>
		await Entities.FirstOrDefaultAsync(p => p.PayOSOrderCode == orderCode);

	public async Task<IEnumerable<PaymentModel>> FindByStatus(string status) =>
		await Entities.Where(p => p.Status == status).ToListAsync();

	public async Task<PaymentModel> UpdateStatus(Guid paymentId, string status) {
		var payment = await FindOne(paymentId);
		if (payment == null) {
			throw new InvalidOperationException($"Payment with ID {paymentId} not found");
		}

		var updatedPayment = new PaymentModel(payment) {
			Status = status,
			UpdatedAt = DateTime.UtcNow
		};

		return await Update(updatedPayment);
	}
}