namespace MovieTicket.Api.Controller.Movie;

using Application.Dto.Movie;
using Application.Service.Contract;
using Domain.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(RouteConstant.CONTROLLER)]
public sealed class MovieController(IMovieService service) : ControllerBase, IMovieController {

	[HttpGet("{id:guid}")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<MovieResponseDto>> FindOne(Guid id) {
		var movie = await service.FindOne(id);
		return movie == null ? TypedResults.NotFound<MovieResponseDto>(null) : TypedResults.Ok(new MovieResponseDto(movie));
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> FindAll() {
		var movies = await service.FindAll();
		return TypedResults.Ok(movies.Select(m => new MovieResponseDto(m)));
	}

	[HttpPost]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<MovieResponseDto>> Create([FromBody] MovieRequestDto entity) {
		var movie = await service.Create(entity.ToModel());
		return TypedResults.Created($"/api/v1/movie/{movie.Id}", new MovieResponseDto(movie));
	}

	[HttpPut("{id:guid}")]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<MovieResponseDto>> Update(Guid id, [FromBody] MovieRequestDto entity) {
		var movie = await service.Update(id, entity.ToModel());
		return TypedResults.Ok(new MovieResponseDto(movie));
	}

	[HttpDelete("{id:guid}")]
	[Authorize(RoleConstant.ADMIN)]
	public async Task<IValueHttpResult<int>> Delete(Guid id) {
		var result = await service.Delete(id);
		return TypedResults.Ok(result);
	}

	[HttpGet("genre/{genre}")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> FindByGenre(string genre) {
		var movies = await service.FindByGenre(genre);
		return TypedResults.Ok(movies.Select(m => new MovieResponseDto(m)));
	}

	[HttpGet("search")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> Search([FromQuery] string title) {
		var movies = await service.FindByTitle(title);
		return TypedResults.Ok(movies.Select(m => new MovieResponseDto(m)));
	}

	[HttpGet("rating/{minRating:decimal}")]
	[AllowAnonymous]
	public async Task<IValueHttpResult<IEnumerable<MovieResponseDto>>> FindByRating(decimal minRating) {
		var movies = await service.FindByRating(minRating);
		return TypedResults.Ok(movies.Select(m => new MovieResponseDto(m)));
	}
}