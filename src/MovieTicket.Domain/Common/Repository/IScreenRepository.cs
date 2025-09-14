namespace MovieTicket.Domain.Common.Repository;

using Entity.Theater;

public interface IScreenRepository : IRepository<ScreenModel> {
	public Task<IEnumerable<ScreenModel>> FindByTheaterId(Guid theaterId);
	public Task<ScreenModel?> FindByTheaterAndScreenNumber(Guid theaterId, int screenNumber);
	public Task<IEnumerable<ScreenModel>> FindByScreenType(string screenType);
}