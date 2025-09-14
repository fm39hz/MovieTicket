namespace MovieTicket.Api.Controller.Theater;

using Application.Dto.Theater;
using Contract;
using Domain.Entity.Theater;

public interface ITheaterController : ICrudController<TheaterModel, TheaterResponseDto, TheaterRequestDto> {
	public Task<IValueHttpResult<IEnumerable<TheaterResponseDto>>> FindByCity(string city);
	public Task<IValueHttpResult<IEnumerable<TheaterResponseDto>>> Search(string name);
	public Task<IValueHttpResult<IEnumerable<TheaterResponseDto>>> FindWithParking();
}
