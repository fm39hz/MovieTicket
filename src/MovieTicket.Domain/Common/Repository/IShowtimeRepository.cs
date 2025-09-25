namespace MovieTicket.Domain.Common.Repository;

using Entity.Theater;

public interface IShowtimeRepository : IRepository<ShowtimeModel> {
	public Task<IEnumerable<ShowtimeModel>> FindByMovieId(Guid movieId);
	public Task<IEnumerable<ShowtimeModel>> FindByTheaterId(Guid theaterId);
	public Task<IEnumerable<ShowtimeModel>> FindByScreenId(Guid screenId);
	public Task<IEnumerable<ShowtimeModel>> FindByDate(DateTime showDate);
	public Task<IEnumerable<ShowtimeModel>> FindByDateRange(DateTime fromDate, DateTime toDate);
	public Task<IEnumerable<ShowtimeModel>> FindAvailableShowtimes(Guid movieId, DateTime? showDate = null);
	public Task<IEnumerable<ShowtimeModel>> FindNowPlaying();
	public Task<IEnumerable<ShowtimeModel>> FindByMovieAndTheater(Guid movieId, Guid theaterId);
}
