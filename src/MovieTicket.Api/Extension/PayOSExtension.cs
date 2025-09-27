namespace MovieTicket.Api.Extension;

using Infrastructure.PayOS;
using Net.payOS;
using JetBrains.Annotations;

public static class PayOSExtension {
	[UsedImplicitly]
	public static IServiceCollection AddPayOS(this IServiceCollection services, IConfiguration configuration) {
		var clientId = configuration["PayOS:ClientId"] ?? throw new InvalidOperationException("PayOS:ClientId is not configured");
		var apiKey = configuration["PayOS:ApiKey"] ?? throw new InvalidOperationException("PayOS:ApiKey is not configured");
		var checksumKey = configuration["PayOS:ChecksumKey"] ?? throw new InvalidOperationException("PayOS:ChecksumKey is not configured");

		var payOS = new PayOS(clientId, apiKey, checksumKey);
		services.AddSingleton(payOS);

		return services;
	}
}