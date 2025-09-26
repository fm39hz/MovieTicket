namespace MovieTicket.Api.Controller.User;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Dto.User;
using Application.Service.Contract;
using Domain.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(RouteConstant.CONTROLLER)]
[Authorize(RoleConstant.USER)]
public sealed class UserController(IUserService service) : ControllerBase, IUserController {
	[HttpGet("{id:guid}")]
	public async Task<IValueHttpResult<UserResponseDto>> FindOne(Guid id) {
		var user = await service.FindOne(id);
		return user == null ? TypedResults.NotFound<UserResponseDto>(null) : TypedResults.Ok(new UserResponseDto(user));
	}

	[HttpPut("{id:guid}")]
	public async Task<IValueHttpResult<UserResponseDto>> Update(Guid id, [FromQuery] UserRequestDto entity) =>
		TypedResults.Ok(new UserResponseDto(await service.Update(id, entity.ToModel())));

	[HttpGet("info")]
	public async Task<IValueHttpResult<UserResponseDto>> Info() {
		var claimsIdentity = User.Identity as ClaimsIdentity;
		var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
		if (userIdClaim == null) {
			return TypedResults.NotFound<UserResponseDto>(null);
		}

		if (!Guid.TryParse(userIdClaim.Value, out var userId)) {
			return TypedResults.NotFound<UserResponseDto>(null);
		}

		var user = await service.FindOne(userId);
		return user == null ? TypedResults.NotFound<UserResponseDto>(null) : TypedResults.Ok(new UserResponseDto(user));
	}
}
