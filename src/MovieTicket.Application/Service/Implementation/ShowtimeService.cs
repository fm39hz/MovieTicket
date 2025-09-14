namespace MovieTicket.Application.Service.Implementation;

using Contract;
using Domain.Common.Repository;
using Domain.Entity.Theater;

public sealed class ShowtimeService(IShowtimeRepository repository) : IShowtimeService {
	public async Task<ShowtimeModel?> FindOne(Guid id) => await repository.FindOne(id);

	public async Task<IEnumerable<ShowtimeModel>> FindAll() => await repository.FindAll();

	public async Task<ShowtimeModel> Create(ShowtimeModel entity) => await repository.Create(entity);

	public async Task<ShowtimeModel> Update(Guid id, ShowtimeModel entity) {
		var showtime = new ShowtimeModel(entity) { Id = id };
		return await repository.Update(showtime);
	}

	public async Task<int> Delete(Guid id) => await repository.Delete(id);

	public async Task<IEnumerable<ShowtimeModel>> FindByMovieId(Guid movieId) => await repository.FindByMovieId(movieId);

	public async Task<IEnumerable<ShowtimeModel>> FindByTheaterId(Guid theaterId) => await repository.FindByTheaterId(theaterId);

	public async Task<IEnumerable<ShowtimeModel>> FindByScreenId(Guid screenId) => await repository.FindByScreenId(screenId);

	public async Task<IEnumerable<ShowtimeModel>> FindByDate(DateOnly showDate) => await repository.FindByDate(showDate);

	public async Task<IEnumerable<ShowtimeModel>> FindByDateRange(DateOnly fromDate, DateOnly toDate) => await repository.FindByDateRange(fromDate, toDate);

	public async Task<IEnumerable<ShowtimeModel>> FindAvailableShowtimes(Guid movieId, DateOnly? showDate = null) => await repository.FindAvailableShowtimes(movieId, showDate);

	public async Task<IEnumerable<ShowtimeModel>> FindNowPlaying() => await repository.FindNowPlaying();

	public async Task<IEnumerable<ShowtimeModel>> FindByMovieAndTheater(Guid movieId, Guid theaterId) => await repository.FindByMovieAndTheater(movieId, theaterId);
}
