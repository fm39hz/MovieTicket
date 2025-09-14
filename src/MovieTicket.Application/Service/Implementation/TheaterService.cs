namespace MovieTicket.Application.Service.Implementation;

using Contract;
using Domain.Common.Repository;
using Domain.Entity.Theater;

public sealed class TheaterService(ITheaterRepository repository) : ITheaterService {
	public async Task<TheaterModel?> FindOne(Guid id) => await repository.FindOne(id);

	public async Task<IEnumerable<TheaterModel>> FindAll() => await repository.FindAll();

	public async Task<TheaterModel> Create(TheaterModel entity) => await repository.Create(entity);

	public async Task<TheaterModel> Update(Guid id, TheaterModel entity) {
		var theater = new TheaterModel(entity) { Id = id };
		return await repository.Update(theater);
	}

	public async Task<int> Delete(Guid id) => await repository.Delete(id);

	public async Task<IEnumerable<TheaterModel>> FindByCity(string city) => await repository.FindByCity(city);

	public async Task<IEnumerable<TheaterModel>> FindByState(string state) => await repository.FindByState(state);

	public async Task<IEnumerable<TheaterModel>> SearchByName(string name) => await repository.SearchByName(name);

	public async Task<IEnumerable<TheaterModel>> FindWithParkingAvailable() => await repository.FindWithParkingAvailable();
}