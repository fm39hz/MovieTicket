namespace MovieTicket.Application.Service.Contract;

using Domain.Entity.Theater;

public interface IScreenService : ICrudService<ScreenModel> {
	public Task<IEnumerable<ScreenModel>> FindByTheaterId(Guid theaterId);
	public Task<ScreenModel?> FindByTheaterAndScreenNumber(Guid theaterId, int screenNumber);
	public Task<IEnumerable<ScreenModel>> FindByScreenType(string screenType);
}