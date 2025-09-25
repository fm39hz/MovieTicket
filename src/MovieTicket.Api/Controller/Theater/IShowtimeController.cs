namespace MovieTicket.Api.Controller.Theater;

using Application.Dto.Theater;
using Contract;
using Domain.Entity.Theater;

public interface IShowtimeController : ICrudController<ShowtimeModel, ShowtimeResponseDto, ShowtimeRequestDto> {
	public Task<IValueHttpResult<IEnumerable<ShowtimeResponseDto>>> FindByMovieId(Guid movieId);
	public Task<IValueHttpResult<IEnumerable<ShowtimeResponseDto>>> FindByTheaterId(Guid theaterId);
	public Task<IValueHttpResult<IEnumerable<ShowtimeResponseDto>>> FindNowPlaying();
	public Task<IValueHttpResult<IEnumerable<ShowtimeResponseDto>>> FindAvailable(Guid movieId, DateTime? showDate = null);
}
