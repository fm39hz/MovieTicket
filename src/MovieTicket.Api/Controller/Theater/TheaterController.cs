namespace MovieTicket.Api.Controller.Theater;

using Application.Dto.Theater;
using Application.Service.Contract;
using Domain.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicket.Application.Dto.Common;

[ApiController]
[Route(RouteConstant.CONTROLLER)]
public sealed class TheaterController(ITheaterService service) : ControllerBase, ITheaterController {

	[HttpGet("{id:guid}")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<TheaterResponseDto>> FindOne(Guid id) {
		var theater = await service.FindOne(id);
		return theater == null ? TypedResults.NotFound<TheaterResponseDto>(null) : TypedResults.Ok(new TheaterResponseDto(theater));
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<TheaterResponseDto>>> FindAll([FromQuery] PaginationRequestDto? pagination) {
		var theaters = await service.FindAll();
		return TypedResults.Ok(theaters.Select(t => new TheaterResponseDto(t)));
	}

	[HttpPost]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<TheaterResponseDto>> Create([FromBody] TheaterRequestDto entity) {
		var theater = await service.Create(entity.ToModel());
		return TypedResults.Created($"/api/v1/theater/{theater.Id}", new TheaterResponseDto(theater));
	}

	[HttpPut("{id:guid}")]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<TheaterResponseDto>> Update(Guid id, [FromBody] TheaterRequestDto entity) {
		var theater = await service.Update(id, entity.ToModel());
		return TypedResults.Ok(new TheaterResponseDto(theater));
	}

	[HttpDelete("{id:guid}")]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<int>> Delete(Guid id) {
		var result = await service.Delete(id);
		return TypedResults.Ok(result);
	}

	[HttpGet("city/{city}")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<TheaterResponseDto>>> FindByCity(string city) {
		var theaters = await service.FindByCity(city);
		return TypedResults.Ok(theaters.Select(t => new TheaterResponseDto(t)));
	}

	[HttpGet("search")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<TheaterResponseDto>>> Search([FromQuery] string name) {
		var theaters = await service.SearchByName(name);
		return TypedResults.Ok(theaters.Select(t => new TheaterResponseDto(t)));
	}

	[HttpGet("parking")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<TheaterResponseDto>>> FindWithParking() {
		var theaters = await service.FindWithParkingAvailable();
		return TypedResults.Ok(theaters.Select(t => new TheaterResponseDto(t)));
	}
}