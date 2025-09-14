namespace MovieTicket.Domain.Common.Repository;

using Entity.Theater;

public interface ITheaterRepository : IRepository<TheaterModel> {
	public Task<IEnumerable<TheaterModel>> FindByCity(string city);
	public Task<IEnumerable<TheaterModel>> FindByState(string state);
	public Task<IEnumerable<TheaterModel>> SearchByName(string name);
	public Task<IEnumerable<TheaterModel>> FindWithParkingAvailable();
}