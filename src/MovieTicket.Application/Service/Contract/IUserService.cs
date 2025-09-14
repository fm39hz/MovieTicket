namespace MovieTicket.Application.Service.Contract;

using Domain.Entity.User;

public interface IUserService : ICrudService<UserModel> {
	public Task<UserModel?> FindOne(string email, string password);
	public Task<UserModel?> Register(string name, string email, string password);
}
