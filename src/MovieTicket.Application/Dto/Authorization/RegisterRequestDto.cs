namespace MovieTicket.Application.Dto.Authorization;

using System.ComponentModel.DataAnnotations;

public record RegisterRequestDto {
	[Required]
	[MaxLength(100)]
	public required string Name { get; init; }

	[Required]
	[EmailAddress]
	[MaxLength(200)]
	public required string Email { get; init; }

	[Required]
	[MinLength(6)]
	public required string Password { get; init; }
}