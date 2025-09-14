namespace MovieTicket.Api.Controller.Theater;

using Application.Dto.Theater;
using Application.Service.Contract;
using Domain.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Application.Dto.Common;

[ApiController]
[Route(RouteConstant.CONTROLLER)]
public sealed class ShowtimeController(IShowtimeService service) : ControllerBase, IShowtimeController {

	[HttpGet("{id:guid}")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<ShowtimeResponseDto>> FindOne(Guid id) {
		var showtime = await service.FindOne(id);
		return showtime == null ? TypedResults.NotFound<ShowtimeResponseDto>(null) : TypedResults.Ok(new ShowtimeResponseDto(showtime));
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<ShowtimeResponseDto>>> FindAll([FromQuery] PaginationRequestDto? pagination) {
		var showtimes = await service.FindAll();
		return TypedResults.Ok(showtimes.Select(s => new ShowtimeResponseDto(s)));
	}

	[HttpPost]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<ShowtimeResponseDto>> Create([FromBody] ShowtimeRequestDto entity) {
		var showtime = await service.Create(entity.ToModel());
		return TypedResults.Created($"/api/v1/showtime/{showtime.Id}", new ShowtimeResponseDto(showtime));
	}

	[HttpPut("{id:guid}")]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<ShowtimeResponseDto>> Update(Guid id, [FromBody] ShowtimeRequestDto entity) {
		var showtime = await service.Update(id, entity.ToModel());
		return TypedResults.Ok(new ShowtimeResponseDto(showtime));
	}

	[HttpDelete("{id:guid}")]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<int>> Delete(Guid id) {
		var result = await service.Delete(id);
		return TypedResults.Ok(result);
	}

	[HttpGet("movie/{movieId:guid}")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<ShowtimeResponseDto>>> FindByMovieId(Guid movieId) {
		var showtimes = await service.FindByMovieId(movieId);
		return TypedResults.Ok(showtimes.Select(s => new ShowtimeResponseDto(s)));
	}

	[HttpGet("theater/{theaterId:guid}")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<ShowtimeResponseDto>>> FindByTheaterId(Guid theaterId) {
		var showtimes = await service.FindByTheaterId(theaterId);
		return TypedResults.Ok(showtimes.Select(s => new ShowtimeResponseDto(s)));
	}

	[HttpGet("nowplaying")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<ShowtimeResponseDto>>> FindNowPlaying() {
		var showtimes = await service.FindNowPlaying();
		return TypedResults.Ok(showtimes.Select(s => new ShowtimeResponseDto(s)));
	}

	[HttpGet("available/{movieId:guid}")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<ShowtimeResponseDto>>> FindAvailable(Guid movieId, [FromQuery] DateOnly? showDate = null) {
		var showtimes = await service.FindAvailableShowtimes(movieId, showDate);
		return TypedResults.Ok(showtimes.Select(s => new ShowtimeResponseDto(s)));
	}

	[HttpGet("{id:guid}/seats")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<SeatAvailabilityDto>> GetSeatAvailability(Guid id) {
		var seatAvailability = await service.GetSeatAvailability(id);
		return seatAvailability == null
			? TypedResults.NotFound<SeatAvailabilityDto>(null)
			: TypedResults.Ok(seatAvailability);
	}
}
