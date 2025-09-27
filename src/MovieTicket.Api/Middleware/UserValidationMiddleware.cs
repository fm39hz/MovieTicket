namespace MovieTicket.Api.Middleware;

using System.Security.Claims;
using Controller.User;
using Domain.Constant;
using Microsoft.AspNetCore.Authorization;

public class UserValidationMiddleware(RequestDelegate next) {
	private static readonly Action<ILogger, Guid, Exception?> _dUserIdDenial =
		LoggerMessage.Define<Guid>(LogLevel.Error, new EventId(1, nameof(UserIdDenial)),
			"User id cannot access this scope: {Item}");

	public async Task InvokeAsync(HttpContext context, ILogger<IUserController> logger) {
		var pathSegments = context.Request.Path.Value?.Split('/').Where(s => !string.IsNullOrEmpty(s)).ToArray();
		var user = context.User;
		if (pathSegments is not { Length: > 0 } ||
			!Guid.TryParse(pathSegments[^1], out var parsedId) ||
			user.Identity?.IsAuthenticated != true ||
			HasAllowAnonymousAttribute(context) ||
			user.ValidateScope(parsedId)
			) {
			await next(context);
			return;
		}

		UserIdDenial(logger, parsedId);
		context.Response.StatusCode = 400;
		await context.Response.WriteAsync("Bad Request");
	}

	private static bool HasAllowAnonymousAttribute(HttpContext context) {
		var endpoint = context.GetEndpoint();
		return endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;
	}

	public void UserIdDenial(ILogger logger, Guid userId) =>
		_dUserIdDenial.Invoke(logger, userId, new BadHttpRequestException(""));
}

public static class UserValidationMiddlewareExtensions {
	internal static bool ValidateScope(this ClaimsPrincipal user, Guid id) {
		var userId = new Guid(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
		var role = user.FindFirst(ClaimTypes.Role)!.Value;
		return userId == id || role == RoleConstant.ADMIN;
	}
}
