namespace MovieTicket.Api.Controller.Theater;

using Application.Dto.Theater;
using Contract;
using Domain.Entity.Theater;

public interface IBookingController : ICrudController<BookingModel, BookingResponseDto, BookingRequestDto> {
	public Task<IValueHttpResult<IEnumerable<BookingResponseDto>>> FindByUserId(Guid userId);
	public Task<IValueHttpResult<BookingResponseDto>> FindByReference(string reference);
	public Task<IValueHttpResult<BookingResponseDto>> BookTickets(Guid userId, Guid showtimeId, string[] seatNumbers);
}
