namespace MovieTicket.Application.Dto.Statistics;

public record RevenueStatisticsDto {
	public decimal TotalRevenue { get; init; }
	public decimal MonthlyRevenue { get; init; }
	public decimal WeeklyRevenue { get; init; }
	public decimal DailyRevenue { get; init; }
	public List<MonthlyRevenueDto> MonthlyBreakdown { get; init; } = new();
}

public record MonthlyRevenueDto {
	public int Year { get; init; }
	public int Month { get; init; }
	public string MonthName { get; init; } = string.Empty;
	public decimal Revenue { get; init; }
	public int BookingsCount { get; init; }
}