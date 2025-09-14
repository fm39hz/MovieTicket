namespace MovieTicket.Application.Service.Implementation;

using Contract;
using Domain.Common.Repository;
using Domain.Entity.Theater;

public sealed class ScreenService(IScreenRepository repository) : IScreenService {
	public async Task<ScreenModel?> FindOne(Guid id) => await repository.FindOne(id);

	public async Task<IEnumerable<ScreenModel>> FindAll() => await repository.FindAll();

	public async Task<ScreenModel> Create(ScreenModel entity) => await repository.Create(entity);

	public async Task<ScreenModel> Update(Guid id, ScreenModel entity) {
		var screen = new ScreenModel(entity) { Id = id };
		return await repository.Update(screen);
	}

	public async Task<int> Delete(Guid id) => await repository.Delete(id);

	public async Task<IEnumerable<ScreenModel>> FindByTheaterId(Guid theaterId) => await repository.FindByTheaterId(theaterId);

	public async Task<ScreenModel?> FindByTheaterAndScreenNumber(Guid theaterId, int screenNumber) => await repository.FindByTheaterAndScreenNumber(theaterId, screenNumber);

	public async Task<IEnumerable<ScreenModel>> FindByScreenType(string screenType) => await repository.FindByScreenType(screenType);
}