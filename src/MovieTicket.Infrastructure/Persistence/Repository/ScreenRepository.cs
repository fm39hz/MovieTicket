namespace MovieTicket.Infrastructure.Persistence.Repository;

using Database;
using Domain.Common.Repository;
using Domain.Entity.Theater;
using Microsoft.EntityFrameworkCore;

public sealed class ScreenRepository(ApplicationDbContext context) : CrudRepository<ScreenModel>(context), IScreenRepository {
	public async Task<IEnumerable<ScreenModel>> FindByTheaterId(Guid theaterId) =>
		await Entities
			.Where(s => s.TheaterId == theaterId)
			.OrderBy(s => s.ScreenNumber)
			.ToListAsync();

	public async Task<ScreenModel?> FindByTheaterAndScreenNumber(Guid theaterId, int screenNumber) =>
		await Entities
			.FirstOrDefaultAsync(s => s.TheaterId == theaterId && s.ScreenNumber == screenNumber);

	public async Task<IEnumerable<ScreenModel>> FindByScreenType(string screenType) =>
		await Entities
			.Where(s => s.ScreenType.Contains(screenType, StringComparison.OrdinalIgnoreCase))
			.ToListAsync();
}
