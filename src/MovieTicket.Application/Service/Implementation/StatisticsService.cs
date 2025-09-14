namespace MovieTicket.Application.Service.Implementation;

using Contract;
using Domain.Common.Repository;
using Dto.Statistics;
using System.Globalization;

public sealed class StatisticsService(
	IBookingRepository bookingRepository,
	IUserRepository userRepository,
	IMovieRepository movieRepository,
	ITheaterRepository theaterRepository,
	IShowtimeRepository showtimeRepository) : IStatisticsService {

	public async Task<StatisticsOverviewDto> GetOverview() {
		var bookings = await bookingRepository.FindAll();
		var users = await userRepository.FindAll();
		var movies = await movieRepository.FindAll();
		var theaters = await theaterRepository.FindAll();
		var showtimes = await showtimeRepository.FindAll();

		var totalRevenue = bookings.Sum(b => b.TotalAmount);

		return new StatisticsOverviewDto {
			TotalBookings = bookings.Count(),
			TotalUsers = users.Count(),
			TotalMovies = movies.Count(),
			TotalTheaters = theaters.Count(),
			TotalRevenue = totalRevenue,
			TotalShowtimes = showtimes.Count()
		};
	}

	public async Task<RevenueStatisticsDto> GetRevenueStatistics() {
		var bookings = await bookingRepository.FindAll();
		var now = DateTime.Now;

		var totalRevenue = bookings.Sum(b => b.TotalAmount);
		var monthlyRevenue = bookings
			.Where(b => b.BookingDate.Year == now.Year && b.BookingDate.Month == now.Month)
			.Sum(b => b.TotalAmount);
		var weeklyRevenue = bookings
			.Where(b => b.BookingDate >= now.AddDays(-7))
			.Sum(b => b.TotalAmount);
		var dailyRevenue = bookings
			.Where(b => b.BookingDate.Date == now.Date)
			.Sum(b => b.TotalAmount);

		var monthlyBreakdown = bookings
			.GroupBy(b => new { b.BookingDate.Year, b.BookingDate.Month })
			.Select(g => new MonthlyRevenueDto {
				Year = g.Key.Year,
				Month = g.Key.Month,
				MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
				Revenue = g.Sum(b => b.TotalAmount),
				BookingsCount = g.Count()
			})
			.OrderByDescending(m => m.Year)
			.ThenByDescending(m => m.Month)
			.Take(12)
			.ToList();

		return new RevenueStatisticsDto {
			TotalRevenue = totalRevenue,
			MonthlyRevenue = monthlyRevenue,
			WeeklyRevenue = weeklyRevenue,
			DailyRevenue = dailyRevenue,
			MonthlyBreakdown = monthlyBreakdown
		};
	}

	public async Task<List<PopularMovieDto>> GetPopularMovies(int limit = 10) {
		var bookings = await bookingRepository.FindAll();
		var movies = await movieRepository.FindAll();
		var showtimes = await showtimeRepository.FindAll();

		var movieStats = bookings
			.Join(showtimes, b => b.ShowtimeId, s => s.Id, (b, s) => new { Booking = b, Showtime = s })
			.Join(movies, bs => bs.Showtime.MovieId, m => m.Id, (bs, m) => new { bs.Booking, Movie = m })
			.GroupBy(x => x.Movie)
			.Select(g => new PopularMovieDto {
				MovieId = g.Key.Id,
				Title = g.Key.Title,
				Genre = g.Key.Genre,
				BookingsCount = g.Count(),
				TotalRevenue = g.Sum(x => x.Booking.TotalAmount),
				Rating = g.Key.Rating
			})
			.OrderByDescending(m => m.BookingsCount)
			.Take(limit)
			.ToList();

		return movieStats;
	}
}