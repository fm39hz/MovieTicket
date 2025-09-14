namespace MovieTicket.Api.Controller.User;

using Application.Dto.User;
using Application.Dto.Common;
using Application.Dto.Statistics;
using Application.Service.Contract;
using Domain.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(RouteConstant.CONTROLLER)]
[Authorize(RoleConstant.ADMIN)]
public sealed class AdminController(IUserService service, IStatisticsService statisticsService) : ControllerBase, IAdminController {
	[HttpGet("{id:guid}")]
	public async Task<IValueHttpResult<UserResponseDto>> FindOne(Guid id) {
		var user = await service.FindOne(id);
		return user?.Role.HasFlag(Roles.Admin) != true
			? TypedResults.NotFound<UserResponseDto>(null)
			: TypedResults.Ok(new UserResponseDto(user));
	}

	[HttpGet]
	public async Task<IValueHttpResult<IEnumerable<UserResponseDto>>> FindAll([FromQuery] PaginationRequestDto? pagination) {
		var users = await service.FindAll();
		var adminUsers = users.Where(user => user.Role.HasFlag(Roles.Admin));
		var dtos = adminUsers.Select(user => new UserResponseDto(user)).ToList();
		return TypedResults.Ok(dtos);
	}

	[HttpPost]
	public async Task<IValueHttpResult<UserResponseDto>> Create([FromBody] UserRequestDto entity) {
		var createdUser = await service.Create(entity.ToModel());
		return TypedResults.Created(createdUser.Id.ToString(), new UserResponseDto(createdUser));
	}

	[HttpPut("{id:guid}")]
	public async Task<IValueHttpResult<UserResponseDto>> Update(Guid id, [FromBody] UserRequestDto entity) =>
		TypedResults.Ok(new UserResponseDto(await service.Update(id, entity.ToModel())));

	[HttpDelete("{id:guid}")]
	public async Task<IValueHttpResult<int>> Delete(Guid id) => TypedResults.Ok(await service.Delete(id));

	[HttpGet("statistics/overview")]
	public async Task<IValueHttpResult<StatisticsOverviewDto>> GetStatisticsOverview() {
		var statistics = await statisticsService.GetOverview();
		return TypedResults.Ok(statistics);
	}

	[HttpGet("statistics/revenue")]
	public async Task<IValueHttpResult<RevenueStatisticsDto>> GetRevenueStatistics() {
		var revenue = await statisticsService.GetRevenueStatistics();
		return TypedResults.Ok(revenue);
	}

	[HttpGet("statistics/movies/popular")]
	public async Task<IValueHttpResult<List<PopularMovieDto>>> GetPopularMovies([FromQuery] int limit = 10) {
		var popularMovies = await statisticsService.GetPopularMovies(limit);
		return TypedResults.Ok(popularMovies);
	}
}
