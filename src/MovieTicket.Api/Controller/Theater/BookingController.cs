namespace MovieTicket.Api.Controller.Theater;

using Application.Dto.Theater;
using Application.Service.Contract;
using Domain.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Application.Dto.Common;

[ApiController]
[Route(RouteConstant.CONTROLLER)]
[Authorize(RoleConstant.USER)]
public sealed class BookingController(IBookingService service) : ControllerBase, IBookingController {

	[HttpGet("{id:guid}")]
	public async Task<IValueHttpResult<BookingResponseDto>> FindOne(Guid id) {
		var booking = await service.FindOne(id);
		return booking == null ? TypedResults.NotFound<BookingResponseDto>(null) : TypedResults.Ok(new BookingResponseDto(booking));
	}

	[HttpGet]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<IEnumerable<BookingResponseDto>>> FindAll([FromQuery] PaginationRequestDto? pagination) {
		var bookings = await service.FindAll();
		return TypedResults.Ok(bookings.Select(b => new BookingResponseDto(b)));
	}

	[HttpPost]
	public async Task<IValueHttpResult<BookingResponseDto>> Create([FromBody] BookingRequestDto entity) {
		var booking = await service.Create(entity.ToModel());
		return TypedResults.Created($"/api/v1/booking/{booking.Id}", new BookingResponseDto(booking));
	}

	[HttpPut("{id:guid}")]
	public async Task<IValueHttpResult<BookingResponseDto>> Update(Guid id, [FromBody] BookingRequestDto entity) {
		var booking = await service.Update(id, entity.ToModel());
		return TypedResults.Ok(new BookingResponseDto(booking));
	}

	[HttpDelete("{id:guid}")]
	public async Task<IValueHttpResult<int>> Delete(Guid id) {
		var result = await service.Delete(id);
		return TypedResults.Ok(result);
	}

	[HttpGet("user/{userId:guid}")]
	public async Task<IValueHttpResult<IEnumerable<BookingResponseDto>>> FindByUserId(Guid userId) {
		var bookings = await service.FindByUserId(userId);
		return TypedResults.Ok(bookings.Select(b => new BookingResponseDto(b)));
	}

	[HttpGet("reference/{reference}")]
	public async Task<IValueHttpResult<BookingResponseDto>> FindByReference(string reference) {
		var booking = await service.FindByBookingReference(reference);
		return booking == null ? TypedResults.NotFound<BookingResponseDto>(null) : TypedResults.Ok(new BookingResponseDto(booking));
	}

	[HttpPost("book")]
	public async Task<IValueHttpResult<BookingResponseDto>> BookTickets([FromQuery] Guid userId, [FromQuery] Guid showtimeId, [FromBody] string[] seatNumbers) {
		var booking = await service.BookTickets(userId, showtimeId, seatNumbers);
		return TypedResults.Created($"/api/v1/booking/{booking.Id}", new BookingResponseDto(booking));
	}
}