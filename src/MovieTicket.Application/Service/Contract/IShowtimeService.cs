namespace MovieTicket.Application.Service.Contract;

using Domain.Entity.Theater;
using Dto.Theater;

public interface IShowtimeService : ICrudService<ShowtimeModel> {
	public Task<IEnumerable<ShowtimeModel>> FindByMovieId(Guid movieId);
	public Task<IEnumerable<ShowtimeModel>> FindByTheaterId(Guid theaterId);
	public Task<IEnumerable<ShowtimeModel>> FindByScreenId(Guid screenId);
	public Task<IEnumerable<ShowtimeModel>> FindByDate(DateTime showDate);
	public Task<IEnumerable<ShowtimeModel>> FindByDateRange(DateTime fromDate, DateTime toDate);
	public Task<IEnumerable<ShowtimeModel>> FindAvailableShowtimes(Guid movieId, DateTime? showDate = null);
	public Task<IEnumerable<ShowtimeModel>> FindNowPlaying();
	public Task<IEnumerable<ShowtimeModel>> FindByMovieAndTheater(Guid movieId, Guid theaterId);
	public Task<SeatAvailabilityDto?> GetSeatAvailability(Guid showtimeId);
}
