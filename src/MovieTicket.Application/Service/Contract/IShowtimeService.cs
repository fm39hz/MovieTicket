namespace MovieTicket.Application.Service.Contract;

using Domain.Entity.Theater;

public interface IShowtimeService : ICrudService<ShowtimeModel> {
	public Task<IEnumerable<ShowtimeModel>> FindByMovieId(Guid movieId);
	public Task<IEnumerable<ShowtimeModel>> FindByTheaterId(Guid theaterId);
	public Task<IEnumerable<ShowtimeModel>> FindByScreenId(Guid screenId);
	public Task<IEnumerable<ShowtimeModel>> FindByDate(DateOnly showDate);
	public Task<IEnumerable<ShowtimeModel>> FindByDateRange(DateOnly fromDate, DateOnly toDate);
	public Task<IEnumerable<ShowtimeModel>> FindAvailableShowtimes(Guid movieId, DateOnly? showDate = null);
	public Task<IEnumerable<ShowtimeModel>> FindNowPlaying();
	public Task<IEnumerable<ShowtimeModel>> FindByMovieAndTheater(Guid movieId, Guid theaterId);
}
