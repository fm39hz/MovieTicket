namespace MovieTicket.Application.Service.Contract;

using Domain.Entity.Theater;

public interface ITheaterService : ICrudService<TheaterModel> {
	public Task<IEnumerable<TheaterModel>> FindByCity(string city);
	public Task<IEnumerable<TheaterModel>> FindByState(string state);
	public Task<IEnumerable<TheaterModel>> SearchByName(string name);
	public Task<IEnumerable<TheaterModel>> FindWithParkingAvailable();
}