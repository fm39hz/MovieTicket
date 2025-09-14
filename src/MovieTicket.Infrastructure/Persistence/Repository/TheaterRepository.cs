namespace MovieTicket.Infrastructure.Persistence.Repository;

using Database;
using Domain.Common.Repository;
using Domain.Entity.Theater;
using Microsoft.EntityFrameworkCore;

public sealed class TheaterRepository(ApplicationDbContext context) : CrudRepository<TheaterModel>(context), ITheaterRepository {
	public async Task<IEnumerable<TheaterModel>> FindByCity(string city) =>
		await Entities
			.Where(t => t.City.Contains(city, StringComparison.OrdinalIgnoreCase))
			.ToListAsync();

	public async Task<IEnumerable<TheaterModel>> FindByState(string state) =>
		await Entities
			.Where(t => t.State.Contains(state, StringComparison.OrdinalIgnoreCase))
			.ToListAsync();

	public async Task<IEnumerable<TheaterModel>> SearchByName(string name) =>
		await Entities
			.Where(t => t.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
			.ToListAsync();

	public async Task<IEnumerable<TheaterModel>> FindWithParkingAvailable() =>
		await Entities
			.Where(static t => t.ParkingAvailable)
			.ToListAsync();
}
