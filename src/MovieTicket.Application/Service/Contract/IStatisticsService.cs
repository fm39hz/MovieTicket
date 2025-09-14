namespace MovieTicket.Application.Service.Contract;

using Dto.Statistics;

public interface IStatisticsService {
	public Task<StatisticsOverviewDto> GetOverview();
	public Task<RevenueStatisticsDto> GetRevenueStatistics();
	public Task<List<PopularMovieDto>> GetPopularMovies(int limit = 10);
}