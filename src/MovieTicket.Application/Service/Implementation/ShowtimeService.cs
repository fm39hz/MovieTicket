namespace MovieTicket.Application.Service.Implementation;

using Contract;
using Domain.Common.Repository;
using Domain.Entity.Theater;
using Dto.Theater;

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

	public async Task<IEnumerable<ShowtimeModel>> FindByDate(DateTime showDate) => await repository.FindByDate(showDate);

	public async Task<IEnumerable<ShowtimeModel>> FindByDateRange(DateTime fromDate, DateTime toDate) => await repository.FindByDateRange(fromDate, toDate);

	public async Task<IEnumerable<ShowtimeModel>> FindAvailableShowtimes(Guid movieId, DateTime? showDate = null) => await repository.FindAvailableShowtimes(movieId, showDate);

	public async Task<IEnumerable<ShowtimeModel>> FindNowPlaying() => await repository.FindNowPlaying();

	public async Task<IEnumerable<ShowtimeModel>> FindByMovieAndTheater(Guid movieId, Guid theaterId) => await repository.FindByMovieAndTheater(movieId, theaterId);

	public async Task<SeatAvailabilityDto?> GetSeatAvailability(Guid showtimeId) {
		var showtime = await repository.FindOne(showtimeId);
		if (showtime?.Screen == null) return null;

		var totalSeats = showtime.Screen.TotalSeats;
		var seatLayout = showtime.Screen.SeatLayout;

		// Use booked seats list directly from the model
		var bookedSeatsList = showtime.BookedSeatsList ?? new List<int>();
		var bookedSeatsCount = bookedSeatsList.Count;
		var availableSeats = totalSeats - bookedSeatsCount;

		// Generate all seats list as integers (1, 2, 3, ..., totalSeats)
		var allSeatsList = GenerateAllSeats(totalSeats);

		// Generate available seats list (all seats minus booked seats)
		var availableSeatsList = allSeatsList.Except(bookedSeatsList).ToList();

		return new SeatAvailabilityDto {
			TotalSeats = totalSeats,
			AvailableSeats = availableSeats,
			BookedSeatsCount = bookedSeatsCount,
			BookedSeatsList = bookedSeatsList,
			AllSeatsList = allSeatsList,
			AvailableSeatsList = availableSeatsList,
			SeatLayout = seatLayout
		};
	}

	private static List<int> GenerateAllSeats(int totalSeats) {
		return Enumerable.Range(1, totalSeats).ToList();
	}
}
