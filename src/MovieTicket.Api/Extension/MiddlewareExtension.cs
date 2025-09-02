namespace MovieTicket.Api.Extension;

using JetBrains.Annotations;
using Middleware;

public static class MiddlewareExtension {
	[UsedImplicitly]
	public static IApplicationBuilder UseMiddlewareScope(this IApplicationBuilder builder) {
		builder.UseMiddleware<UserValidationMiddleware>();
		builder.UseMiddleware<PaginationMiddleware>();
		return builder;
	}
}
